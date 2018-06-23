using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Presentation.Commands.Products
{
    public class CreateProductCommand
    {
        public CreateProductCommand(string name, string color, decimal? price)
        {
            Name = name;
            Color = color;
            Price = price;
        }

        public string Name { get; }
        public string Color { get; }
        public decimal? Price { get; }
    }
}
