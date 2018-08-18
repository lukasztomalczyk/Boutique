using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Builders
{
    public class InsuranceBuilder
    {
        private DateTime StartInsurance { get; set; }
        private DateTime EndInsurance { get; set; }
        private List<Insured> Insureds { get; set; }


        public InsuranceBuilder SetStartInsurance(DateTime time)
        {
            StartInsurance = time;
            return this;
        }

        public InsuranceBuilder SetEndInsurance(DateTime time)
        {
            EndInsurance = time;
            return this;
        }

        public InsuranceBuilder SetInsureds(List<Insured> insureds)
        {
            Insureds = insureds;
            return this;
        }


        public Insurance Create()
        {
            return new Insurance(Guid.NewGuid().ToString(), DateTime.Now, StartInsurance, EndInsurance, Insureds);
        }
    }
}