using InsuranceRight.Services.Models.Coverages;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Response
{
    public class VariantsAndCoverages
    {
        public List<ProductVariant> Variants { get; set; }
        public List<Coverage> Coverages { get; set; }
    }
}
