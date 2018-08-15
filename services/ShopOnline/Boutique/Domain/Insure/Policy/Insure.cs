using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Enum;
using Boutique.Domain.Insure.Insureds;

namespace Boutique.Domain.Insure.Policy
{
    public class Insure
    {
        private string Id { get; set; }
        private DateTime DateSubmitting { get; set; }
        private DateTime StartInsurance { get; set; }
        private DateTime EndInsurance { get; set; }
        private List<Insured> Insureds { get; set; }
        private StatusInsurance Status { get; set; }
    }
}