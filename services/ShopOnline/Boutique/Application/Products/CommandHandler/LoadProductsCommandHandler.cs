using Boutique.Presentation.Commands;
using System;
using Boutique.Domain.Interface;
using Cqrs.Handlers;
using System.Threading.Tasks;

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

        public Task<string> HandleAsync(LoadProductsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
