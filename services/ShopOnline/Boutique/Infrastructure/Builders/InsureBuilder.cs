using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Builders
{
    public class InsureBuilder
    {
        public DateTime StartInsurance { get; set; }

        private DateTime DateSubmitting { get; set; }
        public DateTime EndInsurance { get; set; }
        public List<Insured> Insureds { get; set; }
        public Insure Insure;

        public InsureBuilder()
        {
            
        }

        public InsureBuilder SetDateSubmitting()
        {
            DateSubmitting = DateTime.Now;
            return this;
        }

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
            return Insure;
        }
    }
}