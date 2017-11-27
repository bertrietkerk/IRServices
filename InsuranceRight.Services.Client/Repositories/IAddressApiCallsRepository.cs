using InsuranceRight.Services.Client.Models;
using InsuranceRight.Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Client.Repositories
{
    public interface IAddressApiCallsRepository
    {
        Task<ReturnObject<ZipCode>> GetZipCodeReturnObjectAsync(ZipCode zipCode);
        Task<ReturnObject<Address>> GetAddressReturnObjectAsync(Address address);
    }
}
