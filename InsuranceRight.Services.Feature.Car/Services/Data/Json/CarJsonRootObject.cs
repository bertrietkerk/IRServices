using InsuranceRight.Services.Feature.Car.Models;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Services.Data.Json
{
    public class CarJsonRootObject
    {
        public List<CarObject> Cars { get; set; }
    }
}
