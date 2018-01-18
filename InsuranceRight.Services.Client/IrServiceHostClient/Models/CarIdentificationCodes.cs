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

    public partial class CarIdentificationCodes
    {
        /// <summary>
        /// Initializes a new instance of the CarIdentificationCodes class.
        /// </summary>
        public CarIdentificationCodes() { }

        /// <summary>
        /// Initializes a new instance of the CarIdentificationCodes class.
        /// </summary>
        public CarIdentificationCodes(string vehicleIdentificationNumber = default(string), string meldcode = default(string), string registrationCertificateNumber = default(string))
        {
            VehicleIdentificationNumber = vehicleIdentificationNumber;
            Meldcode = meldcode;
            RegistrationCertificateNumber = registrationCertificateNumber;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "vehicleIdentificationNumber")]
        public string VehicleIdentificationNumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "meldcode")]
        public string Meldcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "registrationCertificateNumber")]
        public string RegistrationCertificateNumber { get; set; }

    }
}