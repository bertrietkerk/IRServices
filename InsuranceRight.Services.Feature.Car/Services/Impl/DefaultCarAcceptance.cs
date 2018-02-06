using InsuranceRight.Services.Feature.Car.HelperMethods;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Settings;
using Microsoft.Extensions.Options;

namespace InsuranceRight.Services.Acceptance.Services.Impl
{
    public class DefaultCarAcceptance : ICarAcceptance
    {
        private readonly AcceptanceSettings _settings;
        private readonly ApplicationSettings _sett;

        public DefaultCarAcceptance(IOptions<AcceptanceSettings> settings, IOptions<ApplicationSettings> sett)
        {
            _settings = settings.Value;
            _sett = sett.Value;
        }

        public AcceptanceStatus Check(MostFrequentDriverViewModel driver, CarObject car)
        {
            AcceptanceStatus status;

            if (_settings.AcceptAlways)
            {
                status = new AcceptanceStatus() { IsAccepted = true };
                return status;
            }
            
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

            var expensiveCarBoundary = _settings.ExpensiveCarBoundary;
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

            int claimFree;
            if(!int.TryParse(driver.DamageFreeYears, out claimFree))
            {
                claimFree = 0;
            }

            var driverAge = Helpers.CalculateDriverAge(driver.DateOfBirth);
            var carPrice = car.Price.CatalogPrice;
            var expensiveCarBoundary = _settings.ExpensiveCarBoundary;   

            
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
            if (carPrice >= expensiveCarBoundary && zipcode.StartsWith("1"))
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

            // TODO: for now just accept always

            result.IsAccepted = true;
            return result;
        }
    }
}
