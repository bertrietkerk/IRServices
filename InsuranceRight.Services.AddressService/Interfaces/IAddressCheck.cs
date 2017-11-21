using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public interface IAddressCheck
    {
        bool IsZipCodeValid(string zipCode);
        Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension = "");
    }
}