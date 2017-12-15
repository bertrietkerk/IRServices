﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Coverages;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Enums;
using InsuranceRight.Services.Models.Car;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarPremiumPolicy : ICarPremiumPolicy
    {
        private readonly ILicensePlateLookup _licensePlateLookup;

        private readonly IPremiumCalculator _premiumCalculator;

        public DefaultCarPremiumPolicy(ILicensePlateLookup licensePlateLookup, IPremiumCalculator premiumCalculator)
        {
            _licensePlateLookup = licensePlateLookup;
            _premiumCalculator = premiumCalculator;
        }

        public List<Coverage> GetCoverages(string licensePlate, string ageRange, string claimFreeYear, string zipCode)
        {
            var carAge = GetCarAge(licensePlate);
            var carAgePremium = (carAge / 10M);

            var driverAge = 99;

            if (!string.IsNullOrWhiteSpace(ageRange))
            {
                var ageRangeArray = ageRange.Split(new[] { "-", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (ageRangeArray.Length > 0)
                {
                    var ageRangeLast = int.Parse(ageRangeArray.LastOrDefault().Trim());
                    driverAge = Math.Min(driverAge, ageRangeLast);
                }
            }

           
            var driverAgePremium = (driverAge < 30 ? ((30 - driverAge) / 3M) : (driverAge > 50 ? ((driverAge - 50) / 5M) : 0));

            var claimPremium = 0M;//GetClaimPremium(claimFreeYear);
            var addressPremium = 1M;//GetAddressPremium(zipCode);
           
            // + addressPremium
            var coverages = new List<Coverage>();

            for (var i = 0; i < 30; i++)
            {
                var backOfficecode = string.Format("C{0}", i.ToString("000"));
                coverages.Add(new Coverage()
                {
                    CoverageCode = backOfficecode,
                    CoverageSubCode = backOfficecode,
                    Premium = decimal.Round((1 + ((i * 1.2M) % 3) + carAgePremium + driverAgePremium + claimPremium), 2, MidpointRounding.AwayFromZero)
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
                    return 1.0M;
            }
        }


        public List<ProductVariant> GetVariants_V2(string licensePlate, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            int carAge = GetCarAge(licensePlate);
            CarPrice carPrice = GetCarPrice(licensePlate);


            var mtpl = _premiumCalculator.CalculateMtplPremium(carAge, ageRange, claimFreeYear, zipCode, kmsPerYear);
            var mtplLimitedCasco = _premiumCalculator.CalculateMtplLimitedCascoPremium(carAge, carPrice, ageRange, claimFreeYear, zipCode, kmsPerYear);
            var mtplAllRisk = _premiumCalculator.CalculateMtplAllRiskPremium(carAge, carPrice, ageRange, claimFreeYear, zipCode, kmsPerYear);

            return new List<ProductVariant>() {
                mtpl, mtplLimitedCasco, mtplAllRisk
            };
        }

        
        public List<ProductVariant> GetVariants(string licensePlate, string ageRange, string claimFreeYear, string zipCode)
        {
            var carAge = GetCarAge(licensePlate);
            var carAgePremium = (carAge / 5M);

            var driverAge = 99;

            if (!string.IsNullOrWhiteSpace(ageRange))
            {
                var ageRangeArray = ageRange.Split(new[] { "-", "+", " " }, StringSplitOptions.RemoveEmptyEntries);
                if (ageRangeArray.Length > 0)
                {
                    var ageRangeLast = int.Parse(ageRangeArray.LastOrDefault().Trim());
                    driverAge = Math.Min(driverAge, ageRangeLast);
                }
            }

            #region OldMethod 

            // Age Premium
            //-----------------
            // 20       5.00M
            // 25       2.5M
            // 29       0.5M
            // 30       0M
            // 50       0M
            // 51       0.3M
            // 60       3.3M
            // 80       10.0M
            #endregion
            var driverAgePremium = (driverAge < 30 ? ((30 - driverAge) / 2M) : (driverAge > 50 ? ((driverAge - 50) / 3M) : 0));

            var claimPremium = 1M;//GetClaimPremium(claimFreeYear);
            var addressPremium = 0M;//GetAddressPremium(zipCode);

            var variants = new List<ProductVariant>();

            for (int i = 0; i < 10; i++)
            {
                variants.Add(new ProductVariant()
                {
                    ProductCode = string.Format("V{0}", i.ToString("000")),
                    Premium = decimal.Round((20 + (i * 1.5M) + carAgePremium + driverAgePremium + claimPremium), 2, MidpointRounding.AwayFromZero)
                    //Premium = decimal.Round((20 + (i * 1.5M) + carAgePremium + driverAgePremium + claimPremium + addressPremium), 2, MidpointRounding.AwayFromZero)
                });
            }
            return variants;

        }


        #region private helpermethods


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
                int carYear;
                if (int.TryParse(car.Year, out carYear))
                {
                    carAge = Math.Min(carAge, (DateTime.Today.Year - carYear));
                }
            }
            return carAge;
        }
        #endregion


    }
}
