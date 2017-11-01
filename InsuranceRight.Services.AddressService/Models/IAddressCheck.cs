using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public interface IAddressCheck
    {
        bool ValidateZipCode(string zipCode);

        Address GetFullAddress(string zipCode, string houseNumber);
        Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension);
        Address GetFullAddress(Address address);
    }
}