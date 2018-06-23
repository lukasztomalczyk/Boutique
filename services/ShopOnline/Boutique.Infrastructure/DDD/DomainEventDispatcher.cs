using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Boutique.Infrastructure.DDD
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Run<TEvent>(TEvent @event)
        {
            var handler = _serviceProvider.GetService<IDomainEventHandler<TEvent>>();

            if (handler == null)
                throw new ArgumentException($"Executed event type {@event} doest'n exists in scope services.");

            handler.Run(@event);
        }
    }
}
