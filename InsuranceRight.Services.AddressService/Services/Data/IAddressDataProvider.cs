using InsuranceRight.Services.Models.Foundation;
using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.AddressService.Services.Data
{
    public interface IAddressDataProvider
    {
        IEnumerable<Address> GetValidAddresses();
        IEnumerable<String> GetValidZipCodes(IEnumerable<Address> ValidAddressList);
    }
}
