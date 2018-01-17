using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Models.Response;
using IrServiceHost;

namespace InsuranceRight.Services.Client.Repositories
{
    public class DefaultApiClient : IAddressApiCallsRepository
    {
        private readonly IrServiceHostClient _client;

        public DefaultApiClient()
        {
            _client = new IrServiceHostClient(new Uri("http://localhost:8888"), new AnonymousCredential());
        }


        public async Task<ReturnObject<Address>> GetAddressReturnObjectAsync(Address address)
        {
            // mapp address parameter to address in service
            var _address = new IrServiceHost.Models.Address() { ZipCode = address.ZipCode, HouseNumber = address.HouseNumber, HouseNumberExtension = address.HouseNumberExtension };

            var result = await _client.ApiAddressLookupGetFullAddressPostWithHttpMessagesAsync(_address);
            
            
            // map response to response
            var response = new ReturnObject<Address>()
            {
                Object = new Address()
                {
                    City = result.Body.ObjectProperty.City,
                    Street = result.Body.ObjectProperty.Street,
                    HouseNumber = result.Body.ObjectProperty.HouseNumber,
                    HouseNumberExtension = result.Body.ObjectProperty.HouseNumberExtension,
                    Country = result.Body.ObjectProperty.Country,
                    ZipCode = result.Body.ObjectProperty.ZipCode
                }
            };

            return response;
        }

        public Task<ReturnObject<ZipCode>> GetZipCodeReturnObjectAsync(ZipCode zipCode)
        {
            throw new NotImplementedException();
        }
    }
}