using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Interface;
using RabbitMQ.Settings;

namespace RabbitMQ
{
    public class ConnectionEventBus : IConnectionEventBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private  IConnection _connection;
        private List<string> _serverAddress;

        public ConnectionEventBus(IConnectionFactory connectionFactory, IOptions<EventBusSettings> settings)
        {
            _connectionFactory = connectionFactory;
            _serverAddress = new List<string>() {settings.Value.ServerAddress};
            TryConnect();
        }

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public bool IsConnected()
        {
            return (_connection != null);
        }
            
        public bool TryConnect()
        {
            if (!IsConnected())
            {
              _connection = _connectionFactory.CreateConnection(_serverAddress);
                return true;
            }
            else
            {
                throw new InvalidOperationException("Can't connect do eventbus");
            }
        }
    }
}