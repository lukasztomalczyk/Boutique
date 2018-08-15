using System;

namespace Boutique.Presentation.Commands.Insurance
{
    public class CreateInsuranceCommand
    {
        public DateTime DateSubmitting { get; }
        public DateTime StartInsurance { get; }
        public DateTime EndInsurance { get; }

        public CreateInsuranceCommand(DateTime dateSubmitting, DateTime startInsurance, DateTime endInsurance)
        {
            DateSubmitting = dateSubmitting;
            StartInsurance = startInsurance;
            EndInsurance = endInsurance;
        }
    }
}