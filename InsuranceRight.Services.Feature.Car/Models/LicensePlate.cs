using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class LicensePlate
    {
        [StringLength(8, MinimumLength = 6)]
        public string Licenseplate { get; set; }

        public override string ToString()
        {
            return Licenseplate;
        }

    }
}
