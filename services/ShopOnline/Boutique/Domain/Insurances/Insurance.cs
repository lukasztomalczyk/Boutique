using System;
using System.Collections.Generic;
using Boutique.Domain.Insurances.Enum;
using Boutique.Domain.Insurances.Event;
using Boutique.Domain.Insurances.Insureds;
using DDD.Interfaces;

namespace Boutique.Domain.Insurances
{
    public class Insurance : IEventStore
    {
        private readonly IDomainEventDispatcher Event;

        public string Id { get; private set; }
        public DateTime DateSubmitting { get; private set; }
        public DateTime StartInsurance { get; private set; }
        public DateTime EndInsurance { get; private set; }
        public List<Insured> Insureds { get; private set; }
        public InsuranceStatusEnum Status { get; private set; }
        public List<IDomainEvent> StoredEvents { get; set; } = new List<IDomainEvent>();

        public Insurance(IDomainEventDispatcher domainEventDispatcher)
        {
            Event = domainEventDispatcher;
        }
        public Insurance(string id, DateTime dateSubmitting, DateTime startInsurance, DateTime endInsurance, List<Insured> insureds, InsuranceStatusEnum status)
        {
            Id = id;
            DateSubmitting = dateSubmitting;
            StartInsurance = startInsurance;
            EndInsurance = endInsurance;
            Insureds = insureds;
            Status = status;
        }


        public void AddInsured(Insured newInsured)
        {
            if(newInsured==null) throw new ArgumentNullException();

            var insuredExists = Insureds?.Exists(p => p.Id.Equals(newInsured.Id, StringComparison.OrdinalIgnoreCase)) ?? false;

            if (insuredExists)
                throw new InvalidOperationException("Received insured allready exists .");

            Insureds.Add(newInsured);

            var @event = new InsuredHasBeenCreated(newInsured);
            Event.Dispatch(newInsured);
            StoredEvents.Add(@event);
        }
    }
}