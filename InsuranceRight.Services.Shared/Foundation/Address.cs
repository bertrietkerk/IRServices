using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InsuranceRight.Services.Models.Foundation
{
    public class Address
    {
        public Address() { }

        public Address(string zipCode, string houseNumber, string houseNumberExtension)
        {
            ZipCode = zipCode;
            HouseNumber = houseNumber;
            HouseNumberExtension = houseNumberExtension;
        }

        /// <summary>
        /// The zipcode of the address
        /// </summary>
        [RegularExpression("^[1-9][0-9]{3}[A-Z]{2}$")]
        public string ZipCode { get; set; }

        /// <summary>
        /// The housenumber of the address
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// The housenumber-extension of the address
        /// </summary>
        public string HouseNumberExtension { get; set; }

        /// <summary>
        /// The street of the address
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// The city of the address
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The country of the address
        /// </summary>
        public string Country { get; set; }
    }
}
