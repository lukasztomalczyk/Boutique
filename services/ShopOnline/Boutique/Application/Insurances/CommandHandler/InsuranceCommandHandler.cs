using System;
using System.Threading.Tasks;
using Boutique.Presentation.Commands.Insurance;
using Cqrs.Command;
using Microsoft.Extensions.Logging;

namespace Boutique.Application.Insurances.CommandHandler
{
    public class InsuranceCommandHandler : ICommandHandler<CreateInsuranceCommand, string>
    {
        public InsuranceCommandHandler()
        {
            
        }
        public string Handle(CreateInsuranceCommand command)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> HandleAsync(CreateInsuranceCommand command)
        {
            Console.WriteLine(command);
            return Task.FromResult("ok");
        }
    }
}