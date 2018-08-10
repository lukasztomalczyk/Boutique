using System;
using Boutique.Messages;
using Boutique.Messages.EventBusRabbitMQ;
using Boutique.Messages.EventBusRabbitMQ.Interfaces;

namespace Boutique.Domain.Users.Event
{
    public class UserHasBeenCreatedEvent : IntegrationEvent, IEvent
    {
        public string Id { get; }

        public UserHasBeenCreatedEvent(string id)
        {
            Id = id;
        }
    }
}