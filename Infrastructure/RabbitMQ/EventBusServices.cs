using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Interface;

namespace RabbitMQ
{
    public class EventBusServices : IEventBusServices
    {
        private readonly IConnectionEventBus _connectionEventBus;
        
        private const string  EventBusName = "boutique_event_bus";

        public EventBusServices(IConnectionEventBus connectionEventBus)
        {
            _connectionEventBus = connectionEventBus;
        }

        public void Publish(IEvent @event, string queueName)
        {
            if (!_connectionEventBus.IsConnected()) _connectionEventBus.TryConnect();
            
            using (_connectionEventBus)
            {
                using (var channel = _connectionEventBus.CreateChannel())
                {
                    ExchangeDeclare(channel);
                    channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                    PublishEvent(@event, queueName, channel);
                }
            }
        }

        public List<object> Subscribe(string queueName)
        {
            if (!_connectionEventBus.IsConnected()) _connectionEventBus.TryConnect();
            
            var routing = queueName + ".*";
            
            using (_connectionEventBus)
            {
                using (var channel = _connectionEventBus.CreateChannel())
                {
                    ExchangeDeclare(channel);
                    channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                    channel.QueueBind(queue: queueName, exchange: EventBusName, routingKey: routing);

                    var consumer = new EventingBasicConsumer(channel);
                    var queueMessages = QueueMessages(consumer);

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    return queueMessages;
                }
            }
        }

        private List<object> QueueMessages(EventingBasicConsumer consumer)
        {
            var queueMessages = new List<object>();
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var json = Encoding.UTF8.GetString(body);
                queueMessages.Add(JsonConvert.DeserializeObject(json));
                var routingKey = ea.RoutingKey;
            };
            return queueMessages;
        }

        private void PublishEvent(IEvent @event, string queueName, IModel channel)
        {
            var routing = Routing(@event, queueName);

            channel.BasicPublish(exchange: EventBusName,
                routingKey: routing,
                basicProperties: null,
                body: ConvertEventToSend(@event));
            
            channel.Close();
        }

        private string Routing(IEvent @event, string queueName)
        {
            return queueName + "." + @event.GetType().Name;
        }

        private void ExchangeDeclare(IModel channel)
        {
    
            channel.ExchangeDeclare(exchange: EventBusName,
                type: "topic",
                durable: false,
                autoDelete: false,
                arguments: null);
        }

        private static byte[] ConvertEventToSend(IEvent @event)
        {
            var message = JsonConvert.SerializeObject(@event);
           return Encoding.UTF8.GetBytes(message);
        }
    }
}