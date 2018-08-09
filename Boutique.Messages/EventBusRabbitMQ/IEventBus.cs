namespace Boutique.Messages.EventBusRabbitMQ
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
    }
}