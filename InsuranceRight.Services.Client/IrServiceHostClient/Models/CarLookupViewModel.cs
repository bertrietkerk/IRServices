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

    public partial class CarLookupViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CarLookupViewModel class.
        /// </summary>
        public CarLookupViewModel() { }

        /// <summary>
        /// Initializes a new instance of the CarLookupViewModel class.
        /// </summary>
        public CarLookupViewModel(string brand = default(string), string model = default(string), string edition = default(string))
        {
            Brand = brand;
            Model = model;
            Edition = edition;
        }

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
        [JsonProperty(PropertyName = "edition")]
        public string Edition { get; set; }

    }
}