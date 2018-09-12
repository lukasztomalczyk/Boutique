
using Boutique.Domain.Insurances.Insureds;
using DDD.Interfaces;

namespace Boutique.Domain.Insurances.Event
{
    public class InsuranceEvents : IDomainEvent
    {
        public InsuranceEvents()
        {
            
        }
    }

    public class InsuredHasBeenCreated : InsuranceEvents
    {
        private Insured Insured { get; }

        public InsuredHasBeenCreated(Insured insured)
        {
            Insured = insured;
        }
    }
}