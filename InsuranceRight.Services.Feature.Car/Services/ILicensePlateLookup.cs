using InsuranceRight.Services.Feature.Car.Models;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ILicensePlateLookup
    {
        CarObject GetCar(string licensePlate);
    }
}
