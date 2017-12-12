using InsuranceRight.Services.Models.Coverages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarPremiumPolicy
    {
        List<ProductVariant> GetVariants(string licensePlate, string ageRange, string claimFreeYear, string postCode);
        List<Coverage> GetCoverages(string licensePlate, string ageRange, string claimFreeYear, string postCode);

        // change
        dynamic GetPaymentFrequencyDiscount(int? paymenrFrequency);
    }
}
