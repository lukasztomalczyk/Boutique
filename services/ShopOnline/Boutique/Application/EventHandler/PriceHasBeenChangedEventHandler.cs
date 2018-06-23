using Boutique.Domain.Event;
using Boutique.Infrastructure.DDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Application.EventHandler
{
    public class PriceHasBeenChangedEventHandler : IDomainEventHandler<PriceHasBeenChangedEvent>
    {
        public PriceHasBeenChangedEventHandler()
        {

        }

        public void Run(PriceHasBeenChangedEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
