using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.Impl;

namespace RabbitMQ
{
    public class RabbitPublisher
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;

        public RabbitPublisher ConnectionFactory(string connectionString)
        {
            _factory = new ConnectionFactory()
            {
                HostName = connectionString
            };
            return this;
        }

        public RabbitPublisher OpenConnection()
        {
            _connection = _factory.CreateConnection();
            return this;
        }

        public RabbitPublisher CreateModel()
        {
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "boutique_events",
                                    type: "topic");
            return this;
        }

        public void SendMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
        }

        public void SetSubscriber(string[] args)
        {
            foreach (var bindingKey in args)
            {
                _channel.QueueBind(queue: _channel.QueueDeclare().QueueName.ToString(),
                                   exchange: "boutique_events",
                                   routingKey: bindingKey);
            }
        }
    }
}