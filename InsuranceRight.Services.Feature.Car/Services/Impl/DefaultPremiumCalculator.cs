using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Coverages;
using InsuranceRight.Services.Models.Enums;
using InsuranceRight.Services.Models.Car;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultPremiumCalculator : IPremiumCalculator
    {
        public ProductVariant CalculateMtplPremium(int carAge, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var carAgePremium = GetCarAgePremium(carAge, CarInsurancePackageType.MTPL);
            var claimPremium = GetClaimPremium(claimFreeYear, CarInsurancePackageType.MTPL);
            var addressPremium = GetAddressPremium(zipCode, CarInsurancePackageType.MTPL);
            var kmsPerYearPremium = GetKmsPerYearPremium(kmsPerYear, CarInsurancePackageType.MTPL);
            var driverAgePremium = GetDriverAgePremium(ageRange, CarInsurancePackageType.MTPL);
            if (!driverAgePremium.HasValue)
            {
                return null;
            }

            var totalPremium = (decimal)(carAgePremium + claimPremium + driverAgePremium + addressPremium + kmsPerYearPremium);
            //var totalPremium = (carAgePremium * 0.10M) + (claimPremium * 0.30M) + (addressPremium * 0.20M) + (driverAgePremium * 0.25M) + (kmsPerYearPremium * 0.15M);

            var variant = new ProductVariant()
            {
                InsuranceType = CarInsurancePackageType.MTPL,
                Premium = totalPremium
            };

            return variant;
        }

        public ProductVariant CalculateMtplLimitedCascoPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var carPricePremium = GetCarPricePremium(carPrice, CarInsurancePackageType.MTPL_LimitedCasco);

            var carAgePremium = GetCarAgePremium(carAge, CarInsurancePackageType.MTPL_LimitedCasco);
            var claimPremium = GetClaimPremium(claimFreeYear, CarInsurancePackageType.MTPL_LimitedCasco);
            var addressPremium = GetAddressPremium(zipCode, CarInsurancePackageType.MTPL_LimitedCasco);
            var kmsPerYearPremium = GetKmsPerYearPremium(kmsPerYear, CarInsurancePackageType.MTPL_LimitedCasco);
            var driverAgePremium = GetDriverAgePremium(ageRange, CarInsurancePackageType.MTPL_LimitedCasco);
            if (!driverAgePremium.HasValue)
            {
                return null;
            }

            var totalPremium = (decimal)(carPricePremium + carAgePremium + claimPremium + driverAgePremium + addressPremium + kmsPerYearPremium);
            var limitedCascoPremium = totalPremium * 1.3M;


            var variant = new ProductVariant()
            {
                InsuranceType = CarInsurancePackageType.MTPL_LimitedCasco,
                Premium = limitedCascoPremium
            };

            return variant;
        }

        

        public ProductVariant CalculateMtplAllRiskPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var carPricePremium = 1.2M * (GetCarPricePremium(carPrice, CarInsurancePackageType.MTPL_AllRisk));

            var carAgePremium = GetCarAgePremium(carAge, CarInsurancePackageType.MTPL_AllRisk);
            var claimPremium = GetClaimPremium(claimFreeYear, CarInsurancePackageType.MTPL_AllRisk);
            var addressPremium = GetAddressPremium(zipCode, CarInsurancePackageType.MTPL_AllRisk);
            var kmsPerYearPremium = GetKmsPerYearPremium(kmsPerYear, CarInsurancePackageType.MTPL_AllRisk);
            var driverAgePremium = GetDriverAgePremium(ageRange, CarInsurancePackageType.MTPL_AllRisk);
            if (!driverAgePremium.HasValue)
            {
                return null;
            }

            var totalPremium = (decimal)(carPricePremium + carAgePremium + claimPremium + driverAgePremium + addressPremium + kmsPerYearPremium);
            var allRiskPremium = totalPremium * 2M;

            var variant = new ProductVariant()
            {
                InsuranceType = CarInsurancePackageType.MTPL_AllRisk,
                Premium = allRiskPremium
            };
            return variant;
        }



        #region HelperMethods

        private decimal GetCarPricePremium(CarPrice carPrice, CarInsurancePackageType packageType)
        {
            var price = carPrice.CurrentPrice;
            if (price < 5000)
                return 5M;
            else if (price < 10000)
                return 10M;
            else if (price < 20000)
                return 15M;
            else if (price < 40000)
                return 25M;
            else if (price < 100000)
                return 50M;
            else if (price < 250000)
                return 70M;
            else
                return 100M;
        }



        private int CalculateDriverAge(DateTime dateOfBirth)
        {
            return CalculateDriverAge(dateOfBirth, DateTime.Now);
        }
        private int CalculateDriverAge(DateTime dateOfBirth, DateTime reference)
        {
            int age = reference.Year - dateOfBirth.Year;
            if (reference < dateOfBirth.AddYears(age))
                age--;
            return age;
        }
        private decimal? GetDriverAgePremium(string ageRange, CarInsurancePackageType packageType)
        {
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

            if (driverAge < 18)
                return null;
            else if (driverAge < 20)
                return 20M;
            else if (driverAge < 25)
                return 14M;
            else if (driverAge < 35)
                return 6M;
            else if (driverAge < 55)
                return 10M;
            else if (driverAge < 65)
                return 16M;
            else
                return 19M;

            // return (driverAge < 30 ? ((30 - driverAge) / 2M) : (driverAge > 50 ? ((driverAge - 50) / 3M) : 0));

        }
        private decimal? GetDriverAgePremiumV2(DateTime dateOfBirth, CarInsurancePackageType packageType)
        {
            var driverAge = CalculateDriverAge(dateOfBirth);
            if (driverAge < 18)
                return null;
            else if (driverAge < 20)
                return 50M;
            else if (driverAge < 25)
                return 35M;
            else if (driverAge < 35)
                return 15M;
            else if (driverAge < 55)
                return 30M;
            else if (driverAge < 65)
                return 38M;
            else
                return 45M;

            // return (driverAge < 30 ? ((30 - driverAge) / 2M) : (driverAge > 50 ? ((driverAge - 50) / 3M) : 0));
        }



        private decimal GetCarAgePremium(int carAge, CarInsurancePackageType packageType)
        {
            if (carAge > 10)
            {
                return 1M;
            }

            switch (carAge)
            {
                case 1:
                case 2:
                    return 20M;
                case 3:
                case 4:
                    return 16M;
                case 5:
                case 6:
                    return 12M;
                case 7:
                case 8:
                    return 8M;
                case 9:
                case 10:
                    return 4M;
                default:
                    return 20M;
            }
            //return (carAge < 5 ? 10M : (carAge < 10 ? 5M : 2M));
        }

        private decimal GetKmsPerYearPremium(KilometersPerYear kmsPerYear, CarInsurancePackageType packageType)
        {
            switch (kmsPerYear)
            {
                case KilometersPerYear.LessThan10000:
                    return 1M;
                case KilometersPerYear.Between10000and15000:
                    return 5M;
                case KilometersPerYear.Between15000and20000:
                    return 10M;
                case KilometersPerYear.Between20000and25000:
                    return 15M;
                case KilometersPerYear.MoreThan25000:
                    return 20M;
                case KilometersPerYear.Unknown:
                default:
                    return 0M;
            }
        }

        private decimal GetAddressPremium(string zipCode, CarInsurancePackageType packageType)
        {
            string startsWith = zipCode.Substring(0, 1);
            switch (startsWith)
            {
                case "1": return 10M;
                case "2": return 10M;
                case "3": return 10M;
                case "4": return 2M;
                case "5": return 2M;
                case "6": return 2M;
                case "7": return 2M;
                case "8": return 5M;
                case "9": return 5M;
                default:
                    return 0;
            }
        }

        private decimal? GetClaimPremium(string claimFreeYear, CarInsurancePackageType packageType)
        {
            decimal claimFree;
            if (!decimal.TryParse(claimFreeYear.Trim(), out claimFree))
            { claimFree = (claimFreeYear == "15 or more" ? 15 : 0); }

            if (claimFree <= 3)
                return 20M;
            else if (claimFree <= 6)
                return 15M;
            else if (claimFree <= 10)
                return 10M;
            else if (claimFree <= 14)
                return 5M;
            else if (claimFree == 15)
                return 1M;
            else
                return null;
        }

        #endregion
    }
}
