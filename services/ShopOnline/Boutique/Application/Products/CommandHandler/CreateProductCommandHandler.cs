using Boutique.Presentation.Commands.Products;
using Cqrs.Handlers;
using System;
using System.Threading.Tasks;

namespace Boutique.Application.Products.CommandHandler
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand,string>
    {
        public CreateProductCommandHandler()
        {

        }

        public string Handle(CreateProductCommand command)
        {
            return "Ok";
        }

        public Task<string> HandleAsync(CreateProductCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
