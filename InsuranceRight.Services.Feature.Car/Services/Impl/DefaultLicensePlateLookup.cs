using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Feature.Car.Services.Data;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultLicensePlateLookup : ILicensePlateLookup
    {
        ICarDataProvider _carRepo;

        public DefaultLicensePlateLookup(ICarDataProvider carProvider)
        {
            _carRepo = carProvider;
        }

        public CarObject GetCar(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                return null;
            }

            return _carRepo.GetCars().FirstOrDefault(c => c.LicensePlate.Replace("-", "") == licensePlate.Replace("-", "").ToUpper());
        }
    }
}
