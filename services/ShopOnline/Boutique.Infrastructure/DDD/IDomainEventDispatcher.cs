namespace Boutique.Infrastructure.DDD
{
    public interface IDomainEventDispatcher
    {
        void Run<TEvent>(TEvent @event);
    }
    public interface IDomainEventHandler<TEvent>
    {
        void Run(TEvent @event);
    }
}
