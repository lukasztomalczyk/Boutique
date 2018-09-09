using Boutique.Domain.Insurances.Event;
using DDD.Interfaces;
using System;

namespace Boutique.Application.Insurances.EventHandler
{
    public class NewInsuredCreatedEventHandler : IDomainEventHandler<InsuredHasBeenCreated>
    {
        public NewInsuredCreatedEventHandler()
        {

        }

        public void Handle(InsuredHasBeenCreated @event)
        {
            throw new NotImplementedException();
        }
    }
}
