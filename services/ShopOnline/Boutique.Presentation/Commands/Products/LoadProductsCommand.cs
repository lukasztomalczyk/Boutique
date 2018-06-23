using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Presentation.Commands
{
    public class LoadProductsCommand
    {
        public LoadProductsCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
