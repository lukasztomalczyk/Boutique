using System;

namespace Boutique.Messages.EventBusRabbitMQ
{
    public class IntegrationEvent
    {
        public Guid IdEvent  { get; }
        public DateTime CreationDate { get; }

        protected IntegrationEvent()
        {
            IdEvent = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

    }
}