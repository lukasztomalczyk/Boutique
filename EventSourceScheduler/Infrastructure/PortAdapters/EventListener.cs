using Cqrs.Interface;
using EventSourceScheduler.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourceScheduler.Infrastructure.PortAdapters
{
    internal class EventListener : IListener
    {
        private readonly ILogger _logger;
        private readonly IRabbitMQReadClient _rabbitMQReadClient;
        private readonly IListenerHandler _listenerHandler;
        private readonly EventSourceSettings _settings;

        public EventListener(ILoggerFactory loggerFactory, IRabbitMQReadClient rabbitMQReadClient,IOptions<EventSourceSettings> settings,IListenerHandler listenerHandler)
        {
            _logger = loggerFactory.CreateLogger<EventListener>();
            _rabbitMQReadClient = rabbitMQReadClient;
            _listenerHandler = listenerHandler;
            _settings = settings.Value;
        }

        public void Listen(CancellationToken cancellationToken)
        {
            while (true)
            {
                Task.Run(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogInformation($"Requested to cancel listener");
                        return;
                    }

                    try
                    {
                        _logger.LogInformation($"Message received in EventListener");

                        var message = _rabbitMQReadClient.Read(_settings.QueueName);

                        if (!string.IsNullOrEmpty(message))
                            _listenerHandler.Handle(message);

                        _logger.LogInformation($"Message was successfully handle",message);
                    }
                    catch (Exception ex) 
                    {
                        _logger.LogError($"There was error occurred during handle message",ex.Message);
                        throw;
                    }

                });
            }
        }
    }
}
