using Boutique.Domain;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Application.Products.CommandHandler
{
    public class LoadProductsCommandHandler : ICommandHandler<LoadProductsCommand,string>
    {
        private readonly IProductRepository _productRepository;

        public LoadProductsCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public string Handle(LoadProductsCommand command)
        {
            return _productRepository.Load(command.Id);
        }
    }
}
