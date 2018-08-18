using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Enum;
using Boutique.Domain.Insure.Insureds;
using Dapper.Contrib.Extensions;

namespace Boutique.Domain.Insure.Policy
{
    public class Insurance
    {
        [Key]
        public string Id { get; }
        public DateTime DateSubmitting { get; }
        public DateTime StartInsurance { get; }
        public DateTime EndInsurance { get; }
        public List<Insured> Insureds { get; }
        public InsuraceStatusEnum Status { get; }

        public Insurance(string id, DateTime dateSubmitting, DateTime startInsurance, DateTime endInsurance, List<Insured> insureds)
        {
            Id = Guid.NewGuid().ToString();
            DateSubmitting = dateSubmitting;
            StartInsurance = startInsurance;
            EndInsurance = endInsurance;
            Insureds = insureds;
            Status = InsuraceStatusEnum.Activated;
        }
    }
}