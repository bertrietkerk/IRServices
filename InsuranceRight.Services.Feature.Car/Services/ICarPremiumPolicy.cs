using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Feature.Car.Models.Enums;
using InsuranceRight.Services.Feature.Car.Models.Response;
using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarPremiumPolicy
    {
        List<ProductVariant> GetVariants_Old(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode);
        List<ProductVariant> GetVariants(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear);
        VariantsAndCoverages GetVariantsAndCoverages(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear);
        List<Coverage> GetCoverages(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode);
        
        decimal GetPaymentFrequencyDiscount(int? paymentFrequency);
        CarDiscountPolicy GetDiscountForGroup(string code);
    }
}
