using Boutique.Infrastructure.DDD;
using System;
using System.Collections.Generic;
using System.Text;
using Boutique.Domain.Products.Event;

namespace Boutique.Application.Products.EventHandler
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
