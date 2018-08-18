using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;
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
            return CreateInsurance(command);
        }

        public Task<string> HandleAsync(CreateInsuranceCommand command)
        {
            var newInsuranceId = CreateInsurance(command);
            
            return Task.FromResult(newInsuranceId);
        }

        private string CreateInsurance(CreateInsuranceCommand command)
        {
            var newInsurance = new InsuranceBuilder().SetStartInsurance(command.CoverageStartDate)
               .SetEndInsurance(command.CoverageEndDate)
               .SetInsureds(default(List<Insured>))
               .Create();

            _insuranceRepository.Create(newInsurance);

            return newInsurance.Id;
        }
    }
}