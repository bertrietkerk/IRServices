using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Car
{
    public class CarPrice
    {
        public virtual decimal CatalogPrice { get; set; }

        public virtual decimal CurrentPrice { get; set; }
    }
}
