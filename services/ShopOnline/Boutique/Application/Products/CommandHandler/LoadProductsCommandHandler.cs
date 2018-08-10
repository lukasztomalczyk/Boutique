using Boutique.Domain;
using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Boutique.Domain.Interface;
using Boutique.Domain.Products;

namespace Boutique.Application.Products.CommandHandler
{
    public class LoadProductsCommandHandler : IDomainCommandHandler<LoadProductsCommand,string>
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
