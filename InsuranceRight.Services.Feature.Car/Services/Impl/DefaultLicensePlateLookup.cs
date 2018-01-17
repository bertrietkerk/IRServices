using System.Linq;
using InsuranceRight.Services.Feature.Car.Models;
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
