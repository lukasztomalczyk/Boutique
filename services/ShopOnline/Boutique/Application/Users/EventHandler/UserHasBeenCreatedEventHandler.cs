using System;
using System.Diagnostics;
using Boutique.Domain.Users.Event;
using Boutique.Infrastructure.DDD;

namespace Boutique.Application.Users.EventHandler
{
    public class UserHasBeenCreatedEventHandler : IDomainEventHandler<UserHasBeenCreatedEvent>
    {
        public void Run(UserHasBeenCreatedEvent @event)
        {
            Console.WriteLine($"User has been created: {@event.Id}", "INFO");
        }
    }
}