using System.Collections.Generic;

namespace RabbitMQ.Interface
{
    public interface IEventBusServices
    {
        void Publish(IEvent @event, string queueName);
        List<object> Subscribe(string queueName);
    }
}