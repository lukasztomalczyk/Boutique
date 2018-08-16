using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        IConnection _connection;
        private bool _disposed;
        private readonly List<string> _rabbitMqSettings;

        public RabbitMqConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
          //  _rabbitMqSettings = new List<string>() {rabbitMqSettings.Value.HostName};
        }
        
        public bool IsConnected
        {
            get { return _connection != null && _connection.IsOpen; }
        }
        
        public IModel CreateModel()
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
                _connection =
                    _connectionFactory.CreateConnection(hostnames: _rabbitMqSettings);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}