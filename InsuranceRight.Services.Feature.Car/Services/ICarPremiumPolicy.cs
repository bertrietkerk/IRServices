using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Feature.Car.Models.Enums;
using InsuranceRight.Services.Feature.Car.Models.Response;
using InsuranceRight.Services.Models.Enums;
using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarPremiumPolicy
    {
        List<ProductVariant> GetVariants_Old(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode);
        List<ProductVariant> GetVariants(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear, string groupCodeDiscount = "", PaymentFrequency? paymentFrequency = PaymentFrequency.Monthly);

        VariantsAndCoverages GetVariantsAndCoverages(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipcode, KilometersPerYear kmsPerYear);
        List<Coverage> GetCoverages(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode);
        
        decimal GetPaymentFrequencyDiscount(int? paymentFrequency);
        CarDiscountPolicy GetDiscountForGroup(string code);
    }
}
