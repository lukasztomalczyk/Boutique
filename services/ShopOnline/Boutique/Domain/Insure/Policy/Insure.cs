using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Enum;
using Boutique.Domain.Insure.Insureds;

namespace Boutique.Domain.Insure.Policy
{
    public class Insure
    {
        public string Id { get; }
        public DateTime DateSubmitting { get; }
        public DateTime StartInsurance { get; }
        public DateTime EndInsurance { get; }
        public List<Insured> Insureds { get; }
        public StatusInsurance Status { get; }

        public Insure(string id, DateTime dateSubmitting, DateTime startInsurance, DateTime endInsurance, List<Insured> insureds)
        {
            Id = id;
            DateSubmitting = dateSubmitting;
            StartInsurance = startInsurance;
            EndInsurance = endInsurance;
            Insureds = insureds;
            Status = StatusInsurance.Inactive;
        }
    }
}