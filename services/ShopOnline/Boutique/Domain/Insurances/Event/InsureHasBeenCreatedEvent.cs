using RabbitMQ.Interface;
using RabbitMQ.Interface;

namespace Boutique.Domain.Insurances.Event
{
    public class InsureHasBeenCreatedEvent : EventRoot
    {
        private string Id { get; }
        private string _eventScope = "Insurances";
        
        public InsureHasBeenCreatedEvent(string id)
        {
            Id = id;
        }

        public override string EventScope => _eventScope;
    }
}