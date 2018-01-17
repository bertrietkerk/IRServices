using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class CarObject
    {
        public virtual string LicensePlate { get; set; }

        public virtual string Year { get; set; }

        public virtual string Region { get; set; }

        public virtual string Brand { get; set; }

        public virtual string Model { get; set; }

        public virtual string Fuel { get; set; }

        public virtual string GearType { get; set; }

        public virtual string Engine { get; set; }

        public virtual string BodyWork { get; set; }

        public virtual string Weight { get; set; }

        public virtual string Edition { get; set; }

        public virtual string YearFirstRegistration { get; set; }

        public virtual bool Immobilizer { get; set; }

        public virtual bool Alarm { get; set; }

        public virtual bool MechanicalSecurity { get; set; }

        public virtual bool SatelliteMonitoring { get; set; }

        public virtual string ColorOfCar { get; set; }

        public virtual CarPrice Price { get; set; }

        public virtual bool IsVehicleFound { get; set; }

        public virtual string RegistrationCode { get; set; }

        public virtual string SecurityClass { get; set; }

        public virtual CarInsuredValue InsuredValue { get; set; }

        public virtual CarIdentificationCodes IdentificationCodes { get; set; }
    }
}
