using System;
using System.Collections.Generic;
using Boutique.Domain.Insure.Insureds;
using Boutique.Domain.Insure.Policy;

namespace Boutique.Infrastructure.Builders
{
    public static class InsureBuilder
    {
        private static DateTime StartInsurance { get; set; }
        private static DateTime DateSubmitting { get; set; }
        private static DateTime EndInsurance { get; set; }
        private static List<Insured> Insureds { get; set; }


        public static void SetDateSubmitting()
        {
            DateSubmitting = DateTime.Now;
          
        }

        public static void SetStartInsurance(DateTime time)
        {
            StartInsurance = time;
            
        }

        public static void SetEndInsurance(DateTime time)
        {
            EndInsurance = time;
            
        }

        public static void SetInsureds(List<Insured> insureds)
        {
            Insureds = insureds;
            
        }


        public static Insure Create()
        {
            return new Insure(Guid.NewGuid().ToString(), DateSubmitting, StartInsurance, EndInsurance, Insureds);
        }
    }
}