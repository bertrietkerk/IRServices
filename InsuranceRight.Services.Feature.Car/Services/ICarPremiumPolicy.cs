using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Feature.Car.Models.Enums;
using InsuranceRight.Services.Feature.Car.Models.Response;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarPremiumPolicy
    {
        List<ProductVariant> GetVariants_Old(string licensePlate, string ageRange, string claimFreeYear, string zipCode);
        List<ProductVariant> GetVariants(string licensePlate, string ageRange, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear);
        VariantsAndCoverages GetVariantsAndCoverages(string licensePlate, string ageRange, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear);
        List<Coverage> GetCoverages(string licensePlate, string ageRange, string claimFreeYear, string zipCode);
        
        decimal GetPaymentFrequencyDiscount(int? paymentFrequency);
    }
}
