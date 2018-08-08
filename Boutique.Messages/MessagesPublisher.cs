using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.Impl;

namespace Boutique.Messages
{
    public class MessagesPublisher : IMessagesPublisher
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private string _routingKey;
        private string _exchangeName;

        public MessagesPublisher ConnectionFactory(string connectionString)
        {
            _factory = new ConnectionFactory()
            {
                HostName = connectionString
            };
            return this;
        }

        public MessagesPublisher OpenConnection()
        {
            _connection = _factory.CreateConnection();
            return this;
        }

        public MessagesPublisher CreateModel(string exchangeName)
        {
            _exchangeName = exchangeName;
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _exchangeName,
                type: "topic");
            
            return this;
        }

        public MessagesPublisher SetSubscriber(string sub)
        {
            _routingKey = sub;
            return this;
        }

        public void SendMessage(string message)
        { 
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: _exchangeName,
                routingKey: _routingKey,
                basicProperties: null,
                body: body);
        }
    }
}