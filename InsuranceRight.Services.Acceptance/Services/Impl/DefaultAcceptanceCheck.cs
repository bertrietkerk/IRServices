using System;
using System.Linq;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Car.ViewModels;
using InsuranceRight.Services.Models.HelperMethods;
using InsuranceRight.Services.Models.Settings;
using Microsoft.Extensions.Options;

namespace InsuranceRight.Services.Acceptance.Services.Impl
{
    public class DefaultAcceptanceCheck : IAcceptanceCheck
    {
        private readonly IOptions<ApplicationSettings> _settings;

        public DefaultAcceptanceCheck(IOptions<ApplicationSettings> settings)
        {
            _settings = settings;
        }

        public AcceptanceStatus Check(MostFrequentDriverViewModel driver, CarObject car)
        {
            AcceptanceStatus status;
            // SECURITY CHECK
            status = IsCarSecurityAccepted(car.Price.CatalogPrice, car.Immobilizer, car.Alarm, car.MechanicalSecurity, car.SatelliteMonitoring);
            if (!status.IsAccepted)
                return status;

            // MOST FREQ DRIVER CHECK
            if (driver == null)
                return status;
            status = IsMostFrequentDriverAccepted(driver, car);
            if (!status.IsAccepted)
                return status;

            // RISKS ASSESMENT CHECK
            if (driver.HadDamageOrTheftBefore != null)
                status = IsRiskAssesmentAccepted(driver, car);

            return status;
        }

        private AcceptanceStatus IsCarSecurityAccepted(decimal catalogPrice, bool hasImmobilizer, bool hasAlarm, bool hasMechanicalSecurity, bool hasSatMonitoring)
        {
            var result = new AcceptanceStatus() { IsAccepted = false };
            if (Helpers.IsAnyObjectNull(new bool[] { hasAlarm, hasImmobilizer, hasMechanicalSecurity, hasSatMonitoring }))
            {
                result.Reason = "Alarm/Immobilizer/Mechanical security/Satellite monitoring is/are null";
                return result;
            }

            var expensiveCarBoundary = _settings.Value.AcceptanceSettings.ExpensiveCarBoundary;
            if (catalogPrice >= expensiveCarBoundary && !hasImmobilizer && !hasAlarm && !hasMechanicalSecurity && !hasSatMonitoring)
            {
                result.Reason = "Car too expensive. Need at least one security measurement";
                return result;
            }

            result.IsAccepted = true;
            return result;
        }

        private AcceptanceStatus IsMostFrequentDriverAccepted(MostFrequentDriverViewModel driver, CarObject car)
        {
            var result = new AcceptanceStatus() { IsAccepted = false };
            var zipcode = driver.ResidenceAddress.ZipCode;
            var claimFree = int.Parse(driver.DamageFreeYears);
            var driverAge = GetDriverAge(driver.Age);
            var carPrice = car.Price.CatalogPrice;
            var expensiveCarBoundary = _settings.Value.AcceptanceSettings.ExpensiveCarBoundary;   

            
            if ((driverAge - claimFree) < 18)
            {
                result.Reason = string.Format("Cannot be {0} years old and have {1} claimfree years", driverAge, claimFree);
                return result;
            }
            if (carPrice >= expensiveCarBoundary && driverAge < 25)
            {
                result.Reason = string.Format("Cannot be {0} years old and insure a car that has a catalog value of {1}", driverAge, car.Price.CatalogPrice);
                return result;
            }
            if (carPrice > expensiveCarBoundary && zipcode.StartsWith("1"))
            {
                result.Reason = string.Format("Cannot insure a car that has a catalog value of {0} in area with zipcode {1}", carPrice, zipcode);
                return result;
            }

            result.IsAccepted = true;
            return result;
        }

        private AcceptanceStatus IsRiskAssesmentAccepted(MostFrequentDriverViewModel driver, CarObject car)
        {
            var result = new AcceptanceStatus() { IsAccepted = false };
            
            //if (driver.HadDamageOrTheftBefore)
            //{
            //    result.Reason = "Driver had damage or theft before";
            //    return result;
            //}

            result.IsAccepted = true;
            return result;
        }



        private int GetDriverAge(string ageRange)
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
            return driverAge;
        }
    }
}
