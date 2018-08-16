using System;
using RabbitMQ.Interface;

namespace Boutique.Domain.Users.Event
{
    public class UserHasBeenCreatedEvent : IEvent
    {
        private string Id { get; }
        public string EventScope = "User";

        public UserHasBeenCreatedEvent(string id)
        {
            Id = id;
        }
    }
}