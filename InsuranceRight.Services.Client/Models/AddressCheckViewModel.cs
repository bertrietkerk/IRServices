using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceRight.Services.Shared.Models;

namespace InsuranceRight.Services.Client.Models
{
    public class AddressCheckViewModel
    {
        public Address Address { get; set; }
        public ReturnObject<ZipCode> ReturnZipCodeObject { get; set; }
        public ReturnObject<Address> ReturnAddressObject { get; set; }

    }
}