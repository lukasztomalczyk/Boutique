using Boutique.Infrastructure.CQRS.Commands;
using Boutique.Presentation.Commands.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boutique.Application.Products.CommandHandler
{
    public class CreateProductCommandHandler : IDomainCommandHandler<CreateProductCommand>
    {
        public CreateProductCommandHandler()
        {

        }

        public void Run(CreateProductCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
