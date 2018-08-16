namespace RabbitMQ.Interface
{
    public interface IEventBusServices
    {
        void Publish(IEvent @event, string queueName);
        string Subscribe(string queueName);
    }
}