using System;

namespace Boutique.Presentation.Commands.Insurance
{
    public class CreateInsuranceCommand
    {
        public DateTime ConclusionDate { get; }
        public DateTime CoverageStartDate { get; }
        public DateTime CoverageEndDate { get; }

        public CreateInsuranceCommand(DateTime conclusionDate, DateTime coverageStartDate, DateTime coverageEndDate)
        {
            ConclusionDate = conclusionDate;
            CoverageStartDate = coverageStartDate;
            CoverageEndDate = coverageEndDate;
        }
    }
}