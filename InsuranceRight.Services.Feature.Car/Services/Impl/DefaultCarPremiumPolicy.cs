using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Coverages;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarPremiumPolicy : ICarPremiumPolicy
    {
        private readonly ILicensePlateLookup _licensePlateLookup;

        public DefaultCarPremiumPolicy(ILicensePlateLookup licensePlateLookup)
        {
            _licensePlateLookup = licensePlateLookup;
        }

        public List<Coverage> GetCoverages(string licensePlate, string ageRange, string claimFreeYear, string postCode)
        {
            throw new NotImplementedException();
        }

        public dynamic GetPaymentFrequencyDiscount(int? paymenrFrequency)
        {
            throw new NotImplementedException();
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

            var claimPremium = GetClaimPremium(claimFreeYear);
            var addressPremium = GetAddressPremium(zipCode);

            var variants = new List<ProductVariant>();

            for (int i = 0; i < 10; i++)
            {
                variants.Add(new ProductVariant()
                {
                    ProductCode = string.Format("V{0}", i.ToString("000")),
                    Premium = decimal.Round((20 + (i * 1.5M) + carAgePremium + driverAgePremium + claimPremium + addressPremium), 2, MidpointRounding.AwayFromZero)
                });
            }
            return variants;

        }

        private decimal GetAddressPremium(string zipCode)
        {
            decimal premium;
            string startsWith = zipCode.Substring(0, 1);
            switch (startsWith)
            {
                case "1": return 1M;
                case "2": return 2M;
                case "3": return 3M;
                case "4": return 4M;
                case "5": return 5M;
                case "6": return 6M;
                case "7": return 7M;
                case "8": return 8M;
                case "9": return 9M;
                default:
                    return 0;
            }
        }

        private decimal GetClaimPremium(string claimFreeYear)
        {
            decimal claimFree;
            if (!decimal.TryParse(claimFreeYear.Trim(), out claimFree))
            { claimFree = (claimFreeYear == "15 or more" ? 15 : 0); }

            switch (claimFree)
            {
                case 1: case 2: case 3:
                    return 2M;
                case 4: case 5: case 6:
                    return 4M;
                case 7: case 8: case 9: case 10:
                    return 6M;
                case 11: case 12: case 13: case 14:
                    return 8M;
                case 15:
                    return 10M;
                default:
                    return 0M;
            }
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


    }
}
