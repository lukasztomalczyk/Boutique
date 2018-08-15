using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Builders
{
    public class InsureBuilder
    {
        private DateTime StartInsurance { get; set; }
        private DateTime EndInsurance { get; set; }
        private List<Insured> Insureds { get; set; }


        public InsureBuilder SetStartInsurance(DateTime time)
        {
            StartInsurance = time;
            return this;
        }

        public InsureBuilder SetEndInsurance(DateTime time)
        {
            EndInsurance = time;
            return this;
        }

        public InsureBuilder SetInsureds(List<Insured> insureds)
        {
            Insureds = insureds;
            return this;
        }


        public Insure Create()
        {
            return new Insure(Guid.NewGuid().ToString(), DateTime.Now, StartInsurance, EndInsurance, Insureds);
        }
    }
}