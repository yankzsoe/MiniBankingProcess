using AutoMapper;
using Identity.Framework.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Transaction.Framework.Domain;
using Transaction.Framework.Services.Interface;
using Transaction.Framework.Types;
using Transaction.WebApi.Models;

namespace Transaction.WebApi.Services {
    public class MessageConsumerService : BackgroundService {
        private readonly AppSettings _appSettings;
        private readonly ILogger<MessageConsumerService> _logger;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public MessageConsumerService(IOptions<AppSettings> appSettings, ILogger<MessageConsumerService> logger, IMapper mapper, IServiceScopeFactory serviceProviderFactory) {
            _appSettings = appSettings.Value;
            _logger = logger;
            _mapper = mapper;
            _serviceScopeFactory = serviceProviderFactory;
            //_transactionService = transactionService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {
                //await _receiveMessageService.RunAsync();
                var factory = new ConnectionFactory() { HostName = _appSettings.RabbitMQ.Hostname };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel()) {
                    channel.QueueDeclare(queue: StringResources.AccountSummaryQueueName,
                                         durable: _appSettings.RabbitMQ.Durable,
                                         exclusive: _appSettings.RabbitMQ.Exclusive,
                                         autoDelete: _appSettings.RabbitMQ.AutoDelete,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (model, ea) => {
                        var body = ea.Body.ToArray();
                        var jsonString = Encoding.UTF8.GetString(body);

                        _logger.LogInformation("Receive Message: {0}", jsonString);

                        try {
                            RegisterModel registerModel = JsonConvert.DeserializeObject<RegisterModel>(jsonString);
                            var result = await ProcessMessage(registerModel, stoppingToken);

                        } catch (Exception ex) {
                            _logger.LogError(ex, $"Error Occured On MessageConsumerService Process: {ex.Message}");
                        }
                    };

                    channel.BasicConsume(queue: StringResources.AccountSummaryQueueName,
                                         autoAck: true,
                                         consumerTag: _appSettings.RabbitMQ.ConsumerTag,
                                         consumer: consumer);
                }

                await Task.Delay(TimeSpan.FromMilliseconds(_appSettings.RabbitMQ.Delay));
            }
        }

        //private async Task<bool> InsertAccountSummary(RegisterModel registerModel) {
        //    var accountSummary = _mapper.Map<AccountSummary>(registerModel);
        //    var result = await _transactionService.Register(accountSummary);
        //    return result;

        //} 

        private async Task<bool> ProcessMessage(RegisterModel registerModel, CancellationToken cancellationToken) {
            using (var scope = _serviceScopeFactory.CreateScope()) {
                var _transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();
                var accountSummary = _mapper.Map<AccountSummary>(registerModel);
                var result = await _transactionService.Register(accountSummary);
                return result;
            }
        }
    }
}
