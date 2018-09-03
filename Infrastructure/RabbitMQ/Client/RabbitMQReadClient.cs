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
    public class RabbitMQReadClient : IRabbitMqReadClient
    {

        private readonly RabbitMqSettings _queueSettings;
        private IModel _sessionChannel;

        public RabbitMQReadClient(IModel connection, IOptions<RabbitMqSettings> queueSettings)
        {
            _sessionChannel = connection;
            _queueSettings = queueSettings.Value;
        }

        public string Read(string queueName)
        {
            using (_sessionChannel)
            {
                _sessionChannel.ExchangeDeclare(exchange: _queueSettings.Name,
                                      type: "topic",
                                      durable: false,
                                      autoDelete: false,
                                      arguments: null);

                _sessionChannel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                _sessionChannel.QueueBind(queue: queueName , exchange: _queueSettings.Name, routingKey: "routing");

                var queueMessages = ConsumeMessage();
                return queueMessages;
            }
        }

        private string ConsumeMessage()
        {
            var consumer = new EventingBasicConsumer(_sessionChannel);
            var queueMessages = new List<object>();

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var json = Encoding.UTF8.GetString(body);
                _sessionChannel.BasicAck(ea.DeliveryTag, false);
            };

            return _sessionChannel.BasicConsume(_queueSettings.QueueName, true, consumer);
        }
    }
}
