using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public class Address : IFormattable
    {
        public Address()
        {
        }

        public string ZipCode{ get; set; }
        public string HouseNumber { get; set; }
        public string HouseNumberExtension { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            // not sure about the impl in IR, 
            // for now just return the default address 
            var defaultAddress = string.Format("{0} {1} {2} {3} {4} {5}", Street, HouseNumber, HouseNumberExtension, ZipCode, City, Country);
            return defaultAddress;
        }
    }
}
