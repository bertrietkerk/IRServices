using InsuranceRight.Services.Feature.Car.Models;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Services.Data
{
    public interface ICarDataProvider
    {
        List<CarObject> GetCars();
    }
}
