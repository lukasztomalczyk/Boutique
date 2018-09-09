using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boutique.Domain.Insurances;
using Boutique.Domain.Insurances.Event;
using Boutique.Domain.Insurances.Insureds;
using Boutique.Domain.Users.Event;
using Boutique.Infrastructure.Builders;
using Boutique.Infrastructure.Repositories;
using Boutique.Presentation.Commands.Insurance;
using Cqrs.Command;
using Microsoft.Extensions.Logging;
using RabbitMQ.Interface;

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
            var newInsurance = NewInsurance(command);

            _insuranceRepository.Save(newInsurance);
            
            return newInsurance.Id;
        }

        public Task<string> HandleAsync(CreateInsuranceCommand command)
        {
            var newInsurance = NewInsurance(command);

            _insuranceRepository.Save(newInsurance);

            return Task.FromResult(newInsurance.Id);
        }

        private static Insurance NewInsurance(CreateInsuranceCommand command)
        {
            var newInsurance = new InsureBuilder()
                .SetStartInsurance(command.CoverageStartDate)
                .SetEndInsurance(command.CoverageEndDate)
                .Build();
            
            return newInsurance;
        }
    }
}