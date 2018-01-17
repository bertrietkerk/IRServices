using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models.Coverages
{
    public class Coverage
    {
        public Coverage()
        {
            DeductibleAmounts = new List<DeductibleAmount>();
            SubCoverages = new List<Coverage>();
        }

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string CoverageCode { get; set; }

        public virtual string CoverageSubCode { get; set; }

        public virtual bool IsDisabled { get; set; }

        public virtual bool IsSelected { get; set; }

        public virtual string ExternalId { get; set; }

        public virtual decimal Premium { get; set; }

        public virtual IList<DeductibleAmount> DeductibleAmounts { get; set; }

        public virtual IList<Coverage> SubCoverages { get; set; }

        public virtual string Icon { get; set; }

        //public int UsageStatistics { get; set; }
    }
}
