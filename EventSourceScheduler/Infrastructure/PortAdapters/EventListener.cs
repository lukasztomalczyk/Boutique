using Cqrs.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Interface;
using RabbitMQ.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.PortAdapters
{
    internal class EventListener : IListener
    {
        private readonly ILogger _logger;
        private readonly IRabbitMqReadClient _rabbitMQReadClient;
        private readonly IListenerHandler _listenerHandler;
        private readonly RabbitMqSettings _settings;

        public EventListener(ILoggerFactory loggerFactory, IRabbitMqReadClient rabbitMQReadClient, IOptions<RabbitMqSettings> settings, IListenerHandler listenerHandler)
        {
            _logger = loggerFactory.CreateLogger<EventListener>();
            _rabbitMQReadClient = rabbitMQReadClient;
            _listenerHandler = listenerHandler;
            _settings = settings.Value;
        }

        public void Listen(CancellationToken cancellationToken)
        {

            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Requested to cancel listener");
                return;
            }

            try
            {
                _logger.LogInformation($"Message received in EventListener");

                _rabbitMQReadClient.Read(message =>
               {
                   if (!string.IsNullOrEmpty(message))
                   {
                       _listenerHandler.Handle(message);
                       _logger.LogInformation($"Message was successfully handle", message);
                   }
               }, _settings.QueueName);

            }
            catch (Exception ex)
            {
                _logger.LogError($"There was error occurred during handle message", ex.Message);
                throw;
            }


        }
    }
}
