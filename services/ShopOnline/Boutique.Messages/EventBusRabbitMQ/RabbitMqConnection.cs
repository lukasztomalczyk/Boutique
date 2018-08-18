using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Boutique.Messages.EventBusRabbitMQ.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        IConnection _connection;
        private readonly List<string> _rabbitMqSettings;
        private readonly ILogger logger;

        public RabbitMqConnection(IConnectionFactory connectionFactory, IOptions<RabbitMqSettings> rabbitMqSettings, ILoggerFactory loggerFactory)
        {
            _connectionFactory = connectionFactory;
            _rabbitMqSettings = new List<string>() {rabbitMqSettings.Value.HostName};
            logger = loggerFactory.CreateLogger<RabbitMqConnection>();
        }
        
        //Zbdęne - skoro masz metodę TryConnect to tam powinna być tam ta logika
        public bool IsConnected
        {
            get { return _connection != null && _connection.IsOpen; }
        }
        
        public IModel InitializeSession()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available !");
            }

            return _connection.CreateModel();
        }

        public bool TryConnect()
        {
            try
            {
                //Nie ważne co bd czy połączy się czy nie, to zawszę true ?
                //Sama metoda mówi Spróbuj się połączyć i tutaj pownień być komunikat o failu(error) lub success (return true)

                _connection =
                    _connectionFactory.CreateConnection(hostnames: _rabbitMqSettings);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message, ex);
                throw;
            }
        }
      
    }
}