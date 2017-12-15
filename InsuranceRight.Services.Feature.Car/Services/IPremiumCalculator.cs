using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Coverages;
using InsuranceRight.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface IPremiumCalculator
    {
        ProductVariant CalculateMtplPremium(int carAge, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear);
        ProductVariant CalculateMtplLimitedCascoPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear);
        ProductVariant CalculateMtplAllRiskPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear);
    }
}
