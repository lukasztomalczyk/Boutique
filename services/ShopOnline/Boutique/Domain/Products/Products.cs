using System;
using Boutique.Domain.Products.Event;
using Boutique.Infrastructure.DDD;

namespace Boutique.Domain.Products
{
    public class Products
    {
        private readonly IDomainEventDispatcher _eventDispatcher;

        private Products() { }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        public decimal Price { get; private set; }

        public Products(string id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
            //_eventDispatcher = eventDispatcher;
        }

        public void SetPrices(decimal price)
        {
            if (price == default(decimal))
                throw new InvalidOperationException();

            Price = price;

            var @event = new PriceHasBeenChangedEvent(this);
            _eventDispatcher.Run(@event);
        }

    }
}
