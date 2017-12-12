using InsuranceRight.Services.Models.Foundation;
using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.AddressService.Interfaces
{
    public interface IAddressDataProvider
    {
        IEnumerable<Address> GetValidAddresses();
        IEnumerable<String> GetValidZipCodes(IEnumerable<Address> ValidAddressList);
    }
}
