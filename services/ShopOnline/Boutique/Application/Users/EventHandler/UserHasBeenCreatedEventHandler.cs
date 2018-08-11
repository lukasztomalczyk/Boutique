using System;
using System.Diagnostics;
using Boutique.Domain.Users.Event;
using Boutique.Infrastructure.DDD;
using Boutique.Messages;
using Boutique.Messages.EventBusRabbitMQ;
using Boutique.Messages.EventBusRabbitMQ.Interfaces;

namespace Boutique.Application.Users.EventHandler
{
    public class UserHasBeenCreatedEventHandler : IDomainEventHandler<UserHasBeenCreatedEvent>
    {
        private readonly IEventBus _eventBusRabbitMq;


        public UserHasBeenCreatedEventHandler(IEventBus eventBusRabbitMq)
        {
            _eventBusRabbitMq = eventBusRabbitMq;
        }
        
        public void Run(UserHasBeenCreatedEvent @event)
        {
            Console.WriteLine($"User has been created: {@event.Id}", "INFO");
           // _eventBusRabbitMq.Publish(@event);
        }
    }
}