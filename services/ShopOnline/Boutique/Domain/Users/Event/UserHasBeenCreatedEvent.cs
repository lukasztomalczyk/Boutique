using System;
using Boutique.Domain.Interface;

namespace Boutique.Domain.Users.Event
{
    public class UserHasBeenCreatedEvent : IEvent
    {
        public string Id { get; }

        public UserHasBeenCreatedEvent(string id)
        {
            Id = id;
        }
    }
}