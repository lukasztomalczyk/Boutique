using System.Collections.Generic;
using System.Linq;
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
        }

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
        
        public bool IsConnected() =>_connection != null && _connection.IsOpen;
            
        public bool TryConnect()
        {
            if (!_connection.IsOpen)
            {
              _connection = _connectionFactory.CreateConnection(_serverAddress);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}