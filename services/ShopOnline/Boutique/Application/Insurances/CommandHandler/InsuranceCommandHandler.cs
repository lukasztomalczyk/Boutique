using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boutique.Domain.Insurances;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;
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
        private readonly IEventBusServices _eventBusServices;

        public InsuranceCommandHandler(IInsuranceRepository insuranceRepository, IEventBusServices eventBusServices)
        {
            _insuranceRepository = insuranceRepository;
            _eventBusServices = eventBusServices;
        }
        public string Handle(CreateInsuranceCommand command)
        {
            var newInsurance = NewInsurance(command);

            _insuranceRepository.Create(newInsurance);
            
            var @event = new UserHasBeenCreatedEvent(newInsurance.GetId());
            _eventBusServices.Publish(@event, @event.EventScope);
            
            return newInsurance.GetId();
        }

        public Task<string> HandleAsync(CreateInsuranceCommand command)
        {
            var newInsurance = NewInsurance(command);

            _insuranceRepository.Create(newInsurance);
            
            var @event = new UserHasBeenCreatedEvent(newInsurance.GetId());
            _eventBusServices.Publish(@event, @event.EventScope);
            
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