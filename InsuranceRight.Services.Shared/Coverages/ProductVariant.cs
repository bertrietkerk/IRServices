using InsuranceRight.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Coverages
{
    public class ProductVariant
    {
        public ProductVariant()
        {
            Coverages = new List<Coverage>();
            AdditionalCoverages = new List<Coverage>();
        }

        public CarInsurancePackageType InsuranceType { get; set; }

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual bool IsDisabled { get; set; }

        public virtual bool IsSelected { get; set; }

        public virtual string ExternalId { get; set; }

        public virtual decimal Premium { get; set; }

        public virtual IList<Coverage> Coverages { get; set; }

        public virtual IList<Coverage> AdditionalCoverages { get; set; }
    }
}