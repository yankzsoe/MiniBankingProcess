using Identity.Framework.Helpers;
using Identity.Framework.Services.Interface;
using Identity.Framework.Types;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Identity.Framework.Services {
    public class SendMessageQueueService : ISendMessageQueueService {
        private readonly AppSettings _appSettings;
        private readonly ILogger<SendMessageQueueService> _logger;

        public SendMessageQueueService(IOptions<AppSettings> appSettings, ILogger<SendMessageQueueService> logger) {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public Task<(bool, string)> SendMessageAsync(RegisterAccountSummaryModel message) {
            var factory = new ConnectionFactory() { HostName = _appSettings.RabbitMQ.Hostname };
            try {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(queue: StringResources.AccountSummaryQueueName,
                                         durable: _appSettings.RabbitMQ.Durable,
                                         exclusive: _appSettings.RabbitMQ.Exclusive,
                                         autoDelete: _appSettings.RabbitMQ.AutoDelete,
                                         arguments: null);
                    var jsonString = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(jsonString);

                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: StringResources.AccountSummaryQueueName,
                                         basicProperties: null,
                                         body: body);

                    _logger.LogInformation("Send Message Successfully: {0}", jsonString);

                }
                return Task.FromResult((true, "Ok"));
            } catch (Exception ex) {
                _logger.LogError(ex, message: ex.Message);
                return Task.FromResult((false, ex.Message));
            }
        }
    }
}
