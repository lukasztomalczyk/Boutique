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
    public class RabbitMQReadClient : IRabbitMQReadClient
    {
        private IConnection _connection;
        private readonly EventQueueSettings _queueSettings;
        private IModel SessionChannel;

        public RabbitMQReadClient(IConnection connection, IOptions<EventQueueSettings> queueSettings)
        {
            _connection = connection;
            _queueSettings = queueSettings.Value;
        }

        public string Read(string queueName)
        {
            using (_connection)
            using (SessionChannel = _connection.CreateModel())
            {
                SessionChannel.ExchangeDeclare(exchange: _queueSettings.Name,
                                      type: "topic",
                                      durable: false,
                                      autoDelete: false,
                                      arguments: null);

                SessionChannel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                SessionChannel.QueueBind(queue: queueName , exchange: _queueSettings.Name, routingKey: "");

                var queueMessages = ConsumeMessage();
                return queueMessages;
            }
        }

        private string ConsumeMessage()
        {
            var consumer = new EventingBasicConsumer(SessionChannel);
            var queueMessages = new List<object>();

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var json = Encoding.UTF8.GetString(body);
                SessionChannel.BasicAck(ea.DeliveryTag, false);
            };

            return SessionChannel.BasicConsume(_queueSettings.QueueName, true, consumer);
        }
    }
}
