using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class CarPrice
    {
        public virtual decimal CatalogPrice { get; set; }

        public virtual decimal CurrentPrice { get; set; }
    }
}
