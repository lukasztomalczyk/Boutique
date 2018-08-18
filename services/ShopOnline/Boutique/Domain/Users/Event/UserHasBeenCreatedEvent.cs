using System;
using Boutique.Messages;
using Boutique.Messages.EventBusRabbitMQ;
using Boutique.Messages.EventBusRabbitMQ.Interfaces;

namespace Boutique.Domain.Users.Event
{
    public class UserHasBeenCreatedEvent : IEvent
    {
        public string Id { get; }
        public string Event { get; }

        public UserHasBeenCreatedEvent(string id)
        {
            Id = id;
            Event = this.GetType().Name;
        }
    }
}