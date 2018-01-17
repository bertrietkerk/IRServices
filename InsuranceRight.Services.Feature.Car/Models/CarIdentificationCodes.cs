using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class CarIdentificationCodes
    {
        public virtual string VehicleIdentificationNumber { get; set; }

        public virtual string Meldcode { get; set; }

        public virtual string RegistrationCertificateNumber { get; set; }
    }
}
