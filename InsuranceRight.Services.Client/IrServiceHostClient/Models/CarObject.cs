﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace IrServiceHost.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class CarObject
    {
        /// <summary>
        /// Initializes a new instance of the CarObject class.
        /// </summary>
        public CarObject() { }

        /// <summary>
        /// Initializes a new instance of the CarObject class.
        /// </summary>
        public CarObject(string licensePlate = default(string), string year = default(string), string region = default(string), string brand = default(string), string model = default(string), string fuel = default(string), string gearType = default(string), string engine = default(string), string bodyWork = default(string), string weight = default(string), string edition = default(string), string yearFirstRegistration = default(string), bool? immobilizer = default(bool?), bool? alarm = default(bool?), bool? mechanicalSecurity = default(bool?), bool? satelliteMonitoring = default(bool?), string colorOfCar = default(string), CarPrice price = default(CarPrice), bool? isVehicleFound = default(bool?), string registrationCode = default(string), string securityClass = default(string), CarInsuredValue insuredValue = default(CarInsuredValue), CarIdentificationCodes identificationCodes = default(CarIdentificationCodes))
        {
            LicensePlate = licensePlate;
            Year = year;
            Region = region;
            Brand = brand;
            Model = model;
            Fuel = fuel;
            GearType = gearType;
            Engine = engine;
            BodyWork = bodyWork;
            Weight = weight;
            Edition = edition;
            YearFirstRegistration = yearFirstRegistration;
            Immobilizer = immobilizer;
            Alarm = alarm;
            MechanicalSecurity = mechanicalSecurity;
            SatelliteMonitoring = satelliteMonitoring;
            ColorOfCar = colorOfCar;
            Price = price;
            IsVehicleFound = isVehicleFound;
            RegistrationCode = registrationCode;
            SecurityClass = securityClass;
            InsuredValue = insuredValue;
            IdentificationCodes = identificationCodes;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "licensePlate")]
        public string LicensePlate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "brand")]
        public string Brand { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "fuel")]
        public string Fuel { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "gearType")]
        public string GearType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "engine")]
        public string Engine { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bodyWork")]
        public string BodyWork { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "edition")]
        public string Edition { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "yearFirstRegistration")]
        public string YearFirstRegistration { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "immobilizer")]
        public bool? Immobilizer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "alarm")]
        public bool? Alarm { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "mechanicalSecurity")]
        public bool? MechanicalSecurity { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "satelliteMonitoring")]
        public bool? SatelliteMonitoring { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "colorOfCar")]
        public string ColorOfCar { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "price")]
        public CarPrice Price { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "isVehicleFound")]
        public bool? IsVehicleFound { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "registrationCode")]
        public string RegistrationCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "securityClass")]
        public string SecurityClass { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "insuredValue")]
        public CarInsuredValue InsuredValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "identificationCodes")]
        public CarIdentificationCodes IdentificationCodes { get; set; }

    }
}
