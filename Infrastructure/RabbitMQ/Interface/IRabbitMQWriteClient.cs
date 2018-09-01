
namespace RabbitMQ.Interface
{
    public interface IRabbitMQWriteClient
    {
        void Write(IEvent @event);
    }
}