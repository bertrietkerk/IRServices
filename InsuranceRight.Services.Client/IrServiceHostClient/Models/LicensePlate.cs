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

    public partial class LicensePlate
    {
        /// <summary>
        /// Initializes a new instance of the LicensePlate class.
        /// </summary>
        public LicensePlate() { }

        /// <summary>
        /// Initializes a new instance of the LicensePlate class.
        /// </summary>
        public LicensePlate(string licenseplateProperty = default(string))
        {
            LicenseplateProperty = licenseplateProperty;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "licenseplate")]
        public string LicenseplateProperty { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (this.LicenseplateProperty != null)
            {
                if (this.LicenseplateProperty.Length > 8)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "LicenseplateProperty", 8);
                }
                if (this.LicenseplateProperty.Length < 6)
                {
                    throw new ValidationException(ValidationRules.MinLength, "LicenseplateProperty", 6);
                }
            }
        }
    }
}
