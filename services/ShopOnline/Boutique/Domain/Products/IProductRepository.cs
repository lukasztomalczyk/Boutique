using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Domain
{
    public interface IProductRepository
    {
        void Save(Products.Products product);
        string Load(string Id);
    }
}
