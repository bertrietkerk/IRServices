using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Models.Response;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Client.Repositories
{
    public interface IAddressApiCallsRepository
    {
        Task<ReturnObject<ZipCode>> GetZipCodeReturnObjectAsync(ZipCode zipCode);
        Task<ReturnObject<Address>> GetAddressReturnObjectAsync(Address address);
    }
}
