using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.Client.Repositories
{
    public class AddressApiCallsRepository : IAddressApiCallsRepository
    {
        static string BaseUrl = @"http://localhost:53491/api/addresslookup/";

        public async Task<ReturnObject<T>> BaseReturnObjectApiCall<T>(StringContent stringContent, string path)
        {
            var returnObject = new ReturnObject<T>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync(path, stringContent);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    returnObject = JsonConvert.DeserializeObject<ReturnObject<T>>(response);
                }
            }
            return returnObject;
        }


        public async Task<ReturnObject<ZipCode>> GetZipCodeReturnObjectAsync(ZipCode zipCode)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(zipCode), Encoding.UTF8, "application/json");
            return await BaseReturnObjectApiCall<ZipCode>(stringContent, "validatezipcode");
        }

        public async Task<ReturnObject<Address>> GetAddressReturnObjectAsync(Address address)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, "application/json");
            return await BaseReturnObjectApiCall<Address>(stringContent, "getfulladdress");
        }
    }
}