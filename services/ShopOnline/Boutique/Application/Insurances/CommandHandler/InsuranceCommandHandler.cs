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
        private readonly IRabbitMqWriteClient _writeClient;

        public InsuranceCommandHandler(IInsuranceRepository insuranceRepository, IRabbitMqWriteClient writeClient)
        {
            _insuranceRepository = insuranceRepository;
            _writeClient = writeClient;
        }
        public string Handle(CreateInsuranceCommand command)
        {
            var newInsurance = NewInsurance(command);

            _insuranceRepository.Create(newInsurance);
            
            var @event = new InsureHasBeenCreatedEvent(newInsurance.GetId());
            _writeClient.Write(@event);
            
            return newInsurance.GetId();
        }

        public Task<string> HandleAsync(CreateInsuranceCommand command)
        {
            var newInsurance = NewInsurance(command);

            _insuranceRepository.Create(newInsurance);
            
            var @event = new InsureHasBeenCreatedEvent(newInsurance.GetId());
            _writeClient.Write(@event);

            return Task.FromResult(newInsurance.GetId());
        }

        private static Insure NewInsurance(CreateInsuranceCommand command)
        {
            var newInsurance = new InsureBuilder().SetStartInsurance(command.CoverageStartDate)
                .SetEndInsurance(command.CoverageEndDate)
                .SetInsureds(default(List<Insured>))
                .Build();
            
            return newInsurance;
        }
    }
}