using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boutique.Domain.Insure.Insureds;
using Boutique.Infrastructure.Builders;
using Boutique.Infrastructure.Repositories;
using Boutique.Presentation.Commands.Insurance;
using Cqrs.Command;
using Microsoft.Extensions.Logging;

namespace Boutique.Application.Insurances.CommandHandler
{
    public class InsuranceCommandHandler : ICommandHandler<CreateInsuranceCommand, string>
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceCommandHandler(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }
        public string Handle(CreateInsuranceCommand command)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> HandleAsync(CreateInsuranceCommand command)
        {
            var newInsurance = new InsureBuilder().SetStartInsurance(command.StartInsurance)
                .SetEndInsurance(command.EndInsurance)
                .SetInsureds(default(List<Insured>))
                .Create();

            _insuranceRepository.Create(newInsurance);
            
            return Task.FromResult("ok");
        }
    }
}