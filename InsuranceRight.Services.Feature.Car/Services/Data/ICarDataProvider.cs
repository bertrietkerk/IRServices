using InsuranceRight.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services.Data
{
    public interface ICarDataProvider
    {
        List<CarObject> GetCars();
    }
}
