using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models.Coverages
{
    public class DeductibleAmount
    {
        public virtual string DeductibleId { get; set; }
        public virtual decimal CoveragePremium { get; set; }
    }
}
