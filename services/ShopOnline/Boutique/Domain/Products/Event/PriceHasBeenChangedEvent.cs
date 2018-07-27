using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Domain.Event
{
    public class PriceHasBeenChangedEvent
    {
        public PriceHasBeenChangedEvent(Products.Products products)
        {
            Products = products;
        }

        public Products.Products  Products { get; }
    }
}
