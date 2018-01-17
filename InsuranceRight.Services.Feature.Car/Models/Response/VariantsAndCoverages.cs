using InsuranceRight.Services.Feature.Car.Models.Coverages;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Models.Response
{
    public class VariantsAndCoverages
    {
        public List<ProductVariant> Variants { get; set; }
        public List<Coverage> Coverages { get; set; }
    }
}
