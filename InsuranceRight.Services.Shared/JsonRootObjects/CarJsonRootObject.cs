using InsuranceRight.Services.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Models.JsonRootObjects
{
    public class CarJsonRootObject
    {
        public List<CarObject> Cars { get; set; }
    }
}
