using InsuranceRight.Services.Models.Car.ViewModels;
using InsuranceRight.Services.Models.Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Car
{
    public class CarPremiumFactors
    {
        public CarPremiumFactors()
        {
            Car = new CarObject();
            Driver = new MostFrequentDriverViewModel()
            {
                ResidenceAddress = new Address(),
                CorrespondenceAddress = new Address()
            };
        }

        //public virtual string LicensePlate { get; set; }
        public virtual string RegularDriver { get; set; }
        public virtual CarObject Car { get; set; }
        public virtual MostFrequentDriverViewModel Driver { get; set; }

    }
}
