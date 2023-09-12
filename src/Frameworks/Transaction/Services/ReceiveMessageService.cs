using Identity.Framework.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Framework.Services.Interface;
using Transaction.Framework.Types;

namespace Transaction.Framework.Services {
    public class ReceiveMessageService : IReceiveMessageService {
        private readonly AppSettings _appSettings;
        private readonly ILogger<ReceiveMessageService> _logger;

        public ReceiveMessageService(IOptions<AppSettings> appSettings, ILogger<ReceiveMessageService> logger) {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public Task RunAsync() {
            var factory = new ConnectionFactory() { HostName = _appSettings.RabbitMQ.Hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.QueueDeclare(queue: StringResources.AccountSummaryQueueName,
                                     durable: _appSettings.RabbitMQ.Durable,
                                     exclusive: _appSettings.RabbitMQ.Exclusive,
                                     autoDelete: _appSettings.RabbitMQ.AutoDelete,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) => {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation(message);
                };
                channel.BasicConsume(queue: StringResources.AccountSummaryQueueName,
                                     autoAck: true,
                                     consumerTag: _appSettings.RabbitMQ.ConsumerTag,
                                     consumer: consumer);
            }
            return Task.CompletedTask;
        }
    }
}
