namespace Boutique.Domain.Products.Event
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
