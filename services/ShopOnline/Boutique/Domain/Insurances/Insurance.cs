using System;
using System.Collections.Generic;
using Boutique.Domain.Insurances.Enum;
using Boutique.Domain.Insurances.Insureds;

namespace Boutique.Domain.Insurances
{
    public class Insure
    {
        // to jest ubezpieczenie? Insureds to lista ubezpieczonych !
        private string Id { get; }
        private DateTime DateSubmitting { get; }
        private DateTime StartInsurance { get; }
        private DateTime EndInsurance { get; }
        private List<Insured> Insureds { get; }
        private InsuranceStatusEnum Status { get; }

        public Insure(string id, DateTime dateSubmitting, DateTime startInsurance, DateTime endInsurance, List<Insured> insureds, InsuranceStatusEnum status)
        {
            Id = id;
            DateSubmitting = dateSubmitting;
            StartInsurance = startInsurance;
            EndInsurance = endInsurance;
            Insureds = insureds;
            Status = status;
        }

        public string GetId() => Id;
        public DateTime GetDateSubmitting() => DateSubmitting;
        public DateTime GetStartInsurance() => StartInsurance;
        public DateTime GetEndInsurance() => EndInsurance;
        public List<Insured> GetInsuredsList() => Insureds;
        private InsuranceStatusEnum GetStatus() => Status;

        public void AddInsured(Insured insured)
        {
            if(insured==null) throw new ArgumentNullException();
            
            Insureds.Add(insured);
            
            //ToDo send event
        }
    }
}