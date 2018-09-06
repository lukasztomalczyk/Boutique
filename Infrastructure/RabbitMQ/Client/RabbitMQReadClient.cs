using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using RabbitMQ.Interface;
using RabbitMQ.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Client
{
    public class RabbitMqReadClient : IRabbitMqReadClient
    {

        private readonly RabbitMqSettings _queueSettings;
        private readonly IModel _sessionChannel;

        public RabbitMqReadClient(IModel connection, IOptions<RabbitMqSettings> queueSettings)
        {
            _sessionChannel = connection;
            _queueSettings = queueSettings.Value;
        }

        public string Read(string queueName)
        {
            using (_sessionChannel)
            {
                _sessionChannel.ExchangeDeclare(exchange: _queueSettings.Name, type: "direct");

                _sessionChannel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);
                _sessionChannel.QueueBind(queue: queueName , exchange: _queueSettings.Name, routingKey: queueName);

                var queueMessages = ConsumeMessage(queueName);
                return queueMessages;
            }
        }

        private string ConsumeMessage(string queueName)
        {
            var consumer = new EventingBasicConsumer(_sessionChannel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var json = Encoding.UTF8.GetString(body);

            };

            return _sessionChannel.BasicConsume(queueName, true, consumer);
        }
    }
}
