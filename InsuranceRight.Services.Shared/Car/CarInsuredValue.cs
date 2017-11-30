using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Car
{
    public class CarInsuredValue
    {
        public virtual decimal NewCarPrice { get; set; }

        public virtual decimal? SumInsured { get; set; }
    }
}
