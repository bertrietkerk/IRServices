using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Coverages;
using InsuranceRight.Services.Models.Enums;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.HelperMethods;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultPremiumCalculator : IPremiumCalculator
    {

        /// <summary>
        /// Calculates the premium for the - MTPL - package based on the given parameters
        /// </summary>
        /// <param name="carAge">Age of the car in years</param> 
        /// <param name="ageRange">Age range of the applicant</param>
        /// <param name="claimFreeYear">Amount of years without claim</param>
        /// <param name="zipCode">Zipcode of the residence-address of the applicant</param>
        /// <param name="kmsPerYear">Estimate of the amount of km's the applicant will drive per year</param>
        /// <returns>The Product variant including calculated premium</returns>
        public ProductVariant CalculateMtplPremium(int carAge, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var totalPremium = GetBaseTotalPremium(carAge, ageRange, claimFreeYear, zipCode, kmsPerYear);
            var mtplPremium = decimal.Round((totalPremium * 0.551M), 2, MidpointRounding.AwayFromZero);

            var variant = new ProductVariant()
            {
                InsuranceType = CarInsurancePackageType.Mtpl,
                Premium = mtplPremium
            };
            return variant;
        }

        /// <summary>
        ///  Calculates the premium for the - MTPL Limited Casco - package based on the given parameters
        /// </summary>
        /// <param name="carAge">Age of the car in years</param> 
        /// <param name="carPrice">Price of the car including CatalogPrice and CurrentPrice</param>
        /// <param name="ageRange">Age range of the applicant</param>
        /// <param name="claimFreeYear">Amount of years without claim</param>
        /// <param name="zipCode">Zipcode of the residence-address of the applicant</param>
        /// <param name="kmsPerYear">Estimate of the amount of km's the applicant will drive per year</param>
        /// <returns>The Product variant including calculated premium</returns>
        public ProductVariant CalculateMtplLimitedCascoPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var carPricePremium = GetCarPricePremium(carPrice);
            var totalPremium = (GetBaseTotalPremium(carAge, ageRange, claimFreeYear, zipCode, kmsPerYear) + carPricePremium);
            var limitedCascoPremium = decimal.Round((totalPremium * 0.705M), 2, MidpointRounding.AwayFromZero);
            
            var variant = new ProductVariant()
            {
                InsuranceType = CarInsurancePackageType.Mtpl_LimitedCasco,
                Premium = limitedCascoPremium
            };

            return variant;
        }

        /// <summary>
        /// Calculates the premium for the - MTPL All Risk - package based on the given parameters
        /// </summary>
        /// <param name="carAge">Age of the car in years</param>
        /// <param name="carPrice">Price of the car including CatalogPrice and CurrentPrice</param>
        /// <param name="ageRange">Age range of the applicant</param>
        /// <param name="claimFreeYear">Amount of years without claim</param>
        /// <param name="zipCode">Zipcode of the residence-address of the applicant</param>
        /// <param name="kmsPerYear">Estimate of the amount of km's the applicant will drive per year</param>
        /// <returns>The Product variant including calculated premium</returns>
        public ProductVariant CalculateMtplAllRiskPremium(int carAge, CarPrice carPrice, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var carPricePremium = 1.21M * (GetCarPricePremium(carPrice));
            var totalPremium = (GetBaseTotalPremium(carAge, ageRange, claimFreeYear, zipCode, kmsPerYear) + carPricePremium);
            var allRiskPremium = decimal.Round((totalPremium * 0.851M), 2, MidpointRounding.AwayFromZero);

            var variant = new ProductVariant()
            {
                InsuranceType = CarInsurancePackageType.Mtpl_AllRisk,
                Premium = allRiskPremium
            };
            return variant;
        }

        #region HelperMethods
        private decimal GetCarPricePremium(CarPrice carPrice)
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

        

        private decimal? GetDriverAgePremium(string ageRange)
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
                return 45M;
            else if (driverAge < 25)
                return 32M;
            else if (driverAge < 35)
                return 15M;
            else if (driverAge < 55)
                return 18M;
            else if (driverAge < 65)
                return 37M;
            else
                return 44M;
        }
        private decimal? GetDriverAgePremiumV2(DateTime dateOfBirth)
        {
            var driverAge = Helpers.CalculateDriverAge(dateOfBirth);
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
        }

        private decimal GetCarAgePremium(int carAge)
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
        }

        private decimal GetKmsPerYearPremium(KilometersPerYear kmsPerYear)
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

        private decimal GetAddressPremium(string zipCode)
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

        private decimal? GetClaimPremium(string claimFreeYear)
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

        private decimal GetBaseTotalPremium(int carAge, string ageRange, string claimFreeYear, string zipCode, KilometersPerYear kmsPerYear)
        {
            var carAgePremium = GetCarAgePremium(carAge);
            var claimPremium = GetClaimPremium(claimFreeYear);
            var addressPremium = GetAddressPremium(zipCode);
            var kmsPerYearPremium = GetKmsPerYearPremium(kmsPerYear);
            var driverAgePremium = GetDriverAgePremium(ageRange);

            return (decimal)(carAgePremium + claimPremium + driverAgePremium + addressPremium + kmsPerYearPremium);
        }
        #endregion
    }
}
