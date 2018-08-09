using System;
using Boutique.Domain.Interface;
using Boutique.Messages;

namespace Boutique.Domain.Users.Event
{
    public class UserHasBeenCreatedEvent : IntegrationEvent
    {
        public string Id { get; }

        public UserHasBeenCreatedEvent(string id)
        {
            Id = id;
        }
    }
}