using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Shared.Models
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

        public string ZipCode { get; set; }
        public string HouseNumber { get; set; }
        public string HouseNumberExtension { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
