using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Domain.Event
{
    public class PriceHasBeenChangedEvent
    {
        public PriceHasBeenChangedEvent(Products products)
        {
            Products = products;
        }

        public Products  Products { get; }
    }
}
