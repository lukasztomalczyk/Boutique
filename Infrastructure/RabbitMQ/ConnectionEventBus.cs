using RabbitMQ.Client;

namespace RabbitMQ
{
    public class ConnectionEventBus : IConnectionEventBus
    {
        private readonly IConnectionFactory _connectionFactory;

        public ConnectionEventBus(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IConnection Connect(string serverAddress)
        {
            return _connectionFactory.CreateConnection(serverAddress);
        }
    }
}