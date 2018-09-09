using DDD.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Implementation
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<T>(T domainEvent)
        {
            var dispatcher = _serviceProvider.GetService<IDomainEventHandler<T>>();

            if (dispatcher == null)
                throw new InvalidOperationException($"Dispatcher {nameof(IDomainEventHandler<T>)} cannot be null");

            dispatcher.Handle(domainEvent);
        }
    }
}
