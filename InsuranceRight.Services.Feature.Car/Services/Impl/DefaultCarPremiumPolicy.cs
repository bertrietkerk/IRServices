using System;
using System.Collections.Generic;
using System.Linq;
using InsuranceRight.Services.Models.Enums;
using InsuranceRight.Services.Feature.Car.HelperMethods;
using InsuranceRight.Services.Feature.Car.Models.Response;
using InsuranceRight.Services.Feature.Car.Models.Enums;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using Microsoft.Extensions.Options;
using InsuranceRight.Services.Models.Settings;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarPremiumPolicy : ICarPremiumPolicy, ICarDiscountPolicy
    {
        private readonly ILicensePlateLookup _licensePlateLookup;
        private readonly IPremiumCalculator _premiumCalculator;
        private readonly IOptions<DiscountSettings> _settings;

        public DefaultCarPremiumPolicy(ILicensePlateLookup licensePlateLookup, IPremiumCalculator premiumCalculator, IOptions<DiscountSettings> settings)
        {
            _licensePlateLookup = licensePlateLookup;
            _premiumCalculator = premiumCalculator;
            _settings = settings;
        }

        public List<Coverage> GetCoverages(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode)
        {
            var carAge = GetCarAge(licensePlate);
            var carAgePremium = (carAge / 10M);
            var driverAge = Helpers.CalculateDriverAge(birthDate);

            var driverAgePremium = (driverAge < 30 ? ((30 - driverAge) / 3M) : (driverAge > 50 ? ((driverAge - 50) / 5M) : 0));
            var claimPremium = 0M;
            var addressPremium = 0M;
            var coverages = new List<Coverage>();

            for (var i = 0; i < 30; i++)
            {
                var backOfficecode = string.Format("C{0}", i.ToString("000"));
                coverages.Add(new Coverage()
                {
                    CoverageCode = backOfficecode,
                    CoverageSubCode = backOfficecode,
                    Premium = decimal.Round((1 + ((i * 1.2M) % 3) + carAgePremium + driverAgePremium + claimPremium + addressPremium), 2, MidpointRounding.AwayFromZero)
                });
            }
            return coverages;
        }

        public decimal GetPaymentFrequencyDiscount(int? paymentFrequency)
        {
            var frequency = (PaymentFrequency)paymentFrequency;
            switch (frequency)
            {
                case PaymentFrequency.Annual:
                    return 4.0M;
                case PaymentFrequency.SemiAnnual:
                    return 2.0M;
                case PaymentFrequency.Quarter:
                    return 1.5M;
                case PaymentFrequency.Monthly:
                    return 1.2M;
                case PaymentFrequency.Unknown:
                default:
                    return 0M;
            }
        }

        public List<ProductVariant> GetVariants(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            int carAge = GetCarAge(licensePlate);
            CarPrice carPrice = GetCarPrice(licensePlate);

            var mtpl = _premiumCalculator.CalculateMtplPremium(carAge, birthDate, claimFreeYear, zipCode, kmsPerYear);
            var mtplLimitedCasco = _premiumCalculator.CalculateMtplLimitedCascoPremium(carAge, carPrice, birthDate, claimFreeYear, zipCode, kmsPerYear);
            var mtplAllRisk = _premiumCalculator.CalculateMtplAllRiskPremium(carAge, carPrice, birthDate, claimFreeYear, zipCode, kmsPerYear);

            return new List<ProductVariant>() {
                mtpl, mtplLimitedCasco, mtplAllRisk
            };
        }

        public List<ProductVariant> GetVariants_Old(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode)
        {
            var carAge = GetCarAge(licensePlate);
            var carAgePremium = (carAge / 5M);

            var driverAge = Helpers.CalculateDriverAge(birthDate);

            var driverAgePremium = (driverAge < 30 ? ((30 - driverAge) / 2M) : (driverAge > 50 ? ((driverAge - 50) / 3M) : 0));
            var claimPremium = 1M;
            var addressPremium = 0M;
            var variants = new List<ProductVariant>();

            for (int i = 0; i < 10; i++)
            {
                variants.Add(new ProductVariant()
                {
                    ProductCode = string.Format("V{0}", i.ToString("000")),
                    //Premium = decimal.Round((20 + (i * 1.5M) + carAgePremium + driverAgePremium + claimPremium), 2, MidpointRounding.AwayFromZero)
                    Premium = decimal.Round((20 + (i * 1.5M) + carAgePremium + driverAgePremium + claimPremium + addressPremium), 2, MidpointRounding.AwayFromZero)
                });
            }
            return variants;
        }

        public VariantsAndCoverages GetVariantsAndCoverages(string licensePlate, DateTime? birthDate, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var variants = GetVariants(licensePlate, birthDate, claimFreeYear, zipCode, kmsPerYear);
            var coverages = GetCoverages(licensePlate, birthDate, claimFreeYear, zipCode);

            return new VariantsAndCoverages() { Variants = variants, Coverages = coverages };
        }

        public CarDiscountPolicy GetDiscountForGroup(string code)
        {
            CarDiscountPolicy response = new CarDiscountPolicy() { Code = code, IsDiscountFound = false, Amount = 0 };

            if (!string.IsNullOrWhiteSpace(code) && _settings.Value.DiscountCodes.TryGetValue(code, out int amount))
            {
                response.IsDiscountFound = true;
                response.Amount = amount;
            }

            return response;
        }

        #region HelperMethods

        private CarPrice GetCarPrice(string licensePlate)
        {
            CarPrice carPrice = new CarPrice();
            var car = _licensePlateLookup.GetCar(licensePlate);
            if (car != null)
            {
                carPrice = car.Price;
            }
            return carPrice;
        }

        private int GetCarAge(string licensePlate)
        {
            var carAge = 99;
            var car = _licensePlateLookup.GetCar(licensePlate);
            if (car != null)
            {
                if (int.TryParse(car.Year, out int carYear))
                {
                    carAge = Math.Min(carAge, (DateTime.Today.Year - carYear));
                }
            }
            return carAge;
        }



        #endregion
    }
}
