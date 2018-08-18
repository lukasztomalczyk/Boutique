using System.Dynamic;
using RabbitMQ.Interface;

namespace Boutique.Domain.Insure.Policy.Event
{
    public class InsureHasBeenCreatedEvent : IEvent
    {
        private string Id { get; }
        public string EventScope = "Insure";
        public string EventName { get; }

        public InsureHasBeenCreatedEvent(string id)
        {
            Id = id;
            EventName = GetType().Name;
        }
        
    }
}