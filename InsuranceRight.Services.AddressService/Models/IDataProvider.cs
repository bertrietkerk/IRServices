using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public interface IDataProvider
    {
        IEnumerable<Address> GetValidAddresses();
        IEnumerable<String> GetValidZipCodes(IEnumerable<Address> ValidAddressList);
    }
}
