using System;
using System.Collections.Generic;
using System.IO;
using Boutique.Domain.Insure.Enum;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Builders
{
    public class InsureBuilder
    {
        private static DateTime StartInsurance { get; set; }
        private static DateTime EndInsurance { get; set; }
        private static List<Insured> Insureds { get; set; }
        private static StatusInsuranceEnum Status { get; set; }

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

        public  InsureBuilder SetInsureds(List<Insured> insureds)
        {
            Insureds = insureds;
           return this;
        }

        public InsureBuilder SetStatus()
        {
            Status = StatusInsuranceEnum.Inactive;
            return this;
        }

        public Insure Build()
        {
            if (StartInsurance==default(DateTime))
                throw new InvalidDataException();
            if (EndInsurance==default(DateTime))
                throw new InvalidDataException();
            if (Insureds == default(List<Insured>))
                throw new InvalidDataException();
            
            return new Insure(Guid.NewGuid().ToString(), DateTime.Now, StartInsurance, EndInsurance, Insureds, Status);
        }
    }
}