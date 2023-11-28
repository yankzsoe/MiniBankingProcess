﻿using Identity.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Identity.WebApi.Middlewares {
    public class ExceptionHandlerMiddleware : IMiddleware {
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
            try {
                await next(context);
            } catch (Exception ex) {
                var message = CreateMessage(context, ex);
                _logger.LogError(message, ex);

                await HandleExceptionAsync(context, ex);
            }
        }

        private string CreateMessage(HttpContext context, Exception e) {
            var message = $"Exception caught in global error handler, exception message: {e.Message}, exception stack: {e.StackTrace}";

            if (e.InnerException != null) {
                message = $"{message}, inner exception message {e.InnerException.Message}, inner exception stack {e.InnerException.StackTrace}";
            }

            return $"{message} RequestId: {context.TraceIdentifier}";
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e) {
            var result = new UserCredentialWithProfileResultModel() { IsSuccessfully = false, Message = e.Message };
            int statusCode;

            if (e is ArgumentException || e is ArgumentNullException) {
                statusCode = StatusCodes.Status400BadRequest;
            } else if (e is Framework.Exceptions.InvalidUsernameOrPasswordException) {
                statusCode = StatusCodes.Status400BadRequest;
            } else if (e is Framework.Exceptions.UsernameAlreadyExistException) {
                statusCode = StatusCodes.Status400BadRequest;
            } else {
                statusCode = StatusCodes.Status500InternalServerError;
                result.Message = "Unknown error, please contact the system admin";
            }

            _logger.LogError(e, e.Message);

            var response = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,

                });
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(response);
        }
    }
}
