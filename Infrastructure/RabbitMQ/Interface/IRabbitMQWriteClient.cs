
namespace RabbitMQ.Interface
{
    public interface IRabbitMqWriteClient
    {
        void Write(EventRoot @event);
    }
}