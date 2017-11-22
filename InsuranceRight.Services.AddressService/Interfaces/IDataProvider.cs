using InsuranceRight.Services.Shared.Models;
using System;
using System.Collections.Generic;

namespace InsuranceRight.Services.AddressService.Interfaces
{
    public interface IDataProvider
    {
        IEnumerable<Address> GetValidAddresses();
        IEnumerable<String> GetValidZipCodes(IEnumerable<Address> ValidAddressList);
    }
}
