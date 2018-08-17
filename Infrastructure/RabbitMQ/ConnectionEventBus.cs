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
        private readonly IConnection _connection;

        public ConnectionEventBus(IConnectionFactory connectionFactory, IOptions<EventBusSettings> settings)
        {
            _connection = connectionFactory.CreateConnection(new List<string>()
                {settings.Value.ServerAddress});
        }

        public IModel CreateChannel()
        {
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}