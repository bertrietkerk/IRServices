using InsuranceRight.Services.Models.Coverages;
using InsuranceRight.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarPremiumPolicy
    {
        List<ProductVariant> GetVariants_Old(string licensePlate, string ageRange, string claimFreeYear, string zipCode);
        List<ProductVariant> GetVariants(string licensePlate, string ageRange, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear);

        List<Coverage> GetCoverages(string licensePlate, string ageRange, string claimFreeYear, string zipCode);
        
        decimal GetPaymentFrequencyDiscount(int? paymentFrequency);
    }
}
