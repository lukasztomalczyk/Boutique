using System;
using System.Collections.Generic;
using Boutique.Presentation.Commands.Auth;

namespace Boutique.Domain.LifeInsurances.Bundle.Cash
{
    public class CashLifeInsurance
    {
        public CashLifeInsurance(string id, DateTime applicatonDate, List<Insured> insureds, StatusInsuredEnum status, DateTime? endProtectkingInsured, DateTime? startProtectingInsured)
        {
            Id = id;
            ApplicatonDate = applicatonDate;
            StartProtectingInsured = startProtectingInsured;
            EndProtectkingInsured = endProtectkingInsured;
            Insureds = insureds;
            Status = status;
        }

        public string Id { get; set; }
        public DateTime ApplicatonDate { get; set; }
        public DateTime? StartProtectingInsured { get; set; }
        public DateTime? EndProtectkingInsured { get; set; }
        public List<Insured> Insureds { get; set; }
        public StatusInsuredEnum Status { get; set; }
        //policyholderId, List<Insured>, status, daty
        // Register,Activated, AddInsured

        public void AddInsured()
        {
            
        }

        public void Register()
        {
            
        }

        public void Activated()
        {
            
        }
        
    }
}