using RabbitMQ.Client;

namespace RabbitMQ
{
    public interface IConnectionEventBus
    {
        IConnection Connect(string serverAddress);
    }
}