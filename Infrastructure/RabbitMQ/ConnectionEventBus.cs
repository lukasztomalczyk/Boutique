using System.Collections.Generic;
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
            var hostList = new List<string>() {settings.Value.ServerAddress};
            _connection = connectionFactory.CreateConnection(hostList);
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