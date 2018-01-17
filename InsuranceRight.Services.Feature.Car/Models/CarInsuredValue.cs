using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class CarInsuredValue
    {
        public virtual decimal NewCarPrice { get; set; }

        public virtual decimal? SumInsured { get; set; }
    }
}
