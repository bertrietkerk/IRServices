using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Feature.Car.Models.Enums;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface IPremiumCalculator
    {
        ProductVariant CalculateMtplPremium(int carAge, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear);
        ProductVariant CalculateMtplLimitedCascoPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear);
        ProductVariant CalculateMtplAllRiskPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear);
    }
}
