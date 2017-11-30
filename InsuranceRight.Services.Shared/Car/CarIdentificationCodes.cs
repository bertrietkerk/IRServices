using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Car
{
    public class CarIdentificationCodes
    {
        public virtual string VehicleIdentificationNumber { get; set; }

        public virtual string Meldcode { get; set; }

        public virtual string RegistrationCertificateNumber { get; set; }
    }
}
