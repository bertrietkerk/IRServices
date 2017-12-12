using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Coverages
{
    public class DeductibleAmount
    {
        public virtual string DeductibleId { get; set; }
        public virtual decimal CoveragePremium { get; set; }
    }
}
