using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Enum;
using Boutique.Domain.Insure.Insureds;

namespace Boutique.Domain.Insure.Policy
{
    public class Insure
    {
        private string Id { get; }
        private DateTime DateSubmitting { get; }
        private DateTime StartInsurance { get; }
        private DateTime EndInsurance { get; }
        private List<Insured> Insureds { get; }
        private StatusInsuranceEnum Status { get; }

        public Insure(string id, DateTime dateSubmitting, DateTime startInsurance, DateTime endInsurance, List<Insured> insureds, StatusInsuranceEnum status)
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
        private StatusInsuranceEnum GetStatus() => Status;

        public void AddInsured(Insured insured)
        {
            if(insured==null) throw new ArgumentNullException();
            
            Insureds.Add(insured);
            
            //ToDo send event
        }
    }
}