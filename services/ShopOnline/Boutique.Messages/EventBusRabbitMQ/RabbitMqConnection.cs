using System;
using System.Collections;
using RabbitMQ.Client;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        IConnection _connection;
        private bool _disposed;

        public RabbitMqConnection()
        {
            _connectionFactory = new ConnectionFactory() { HostName = "172.17.0.3"};
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
                _connection = _connectionFactory.CreateConnection();
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