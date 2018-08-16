using System.ComponentModel.Design;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class EventBusServices
    {
        private readonly IConnectionEventBus _connectionEventBus;

        public EventBusServices(IConnectionEventBus connectionEventBus)
        {
            _connectionEventBus = connectionEventBus;
        }

        public void Publish(IEvent @event, string quequeName)
        {
            using (var connect = _connectionEventBus.Connect("cos"))
            {
                using (var channel = connect.CreateModel())
                {
                    CreateQueque(quequeName, channel);
                    PublishEvent(@event, quequeName, channel);
                }
            }
        }

        private void PublishEvent(IEvent @event, string quequeName, IModel channel)
        {
            var routing = quequeName + "." + @event.GetType().FullName;
            
            channel.BasicPublish(exchange: quequeName,
                routingKey: routing,
                basicProperties: null,
                body: ConvertEventToSend(@event));
        }

        private void CreateQueque(string quequeName, IModel channel)
        {
            channel.ExchangeDeclare(exchange: quequeName,
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