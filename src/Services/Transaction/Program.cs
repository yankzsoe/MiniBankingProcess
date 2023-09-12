using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Transaction.WebApi.Middlewares;
using Transaction.WebApi.Services;
using Transaction.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Transaction.WebApi.Mappers;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransactionFramework(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ExceptionHandlerMiddleware>();
//builder.Services.AddAutoMapper(x => x.AddProfile(new ModelMappingProfile()));
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Fake ATM", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddHostedService<MessageConsumerService>();


var app = builder.Build();

app.UseExceptionHandlerMiddleware();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fake ATM v1");
});

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();