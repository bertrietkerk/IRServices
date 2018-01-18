using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.AddressService.Services.Data.Json;

namespace InsuranceRight.Services.AddressService.Services.Data.Impl
{
    public class DefaultAddressDataProvider : IAddressDataProvider
    {
        List<Address> _addressList;

        public DefaultAddressDataProvider()
        {
            _addressList = new List<Address>();
        }

        public IEnumerable<Address> GetValidAddresses()
        {
            AddressJsonRootObject rootObject;

            //using (StreamReader sr = File.OpenText(@".\Json\addresses.json"))
            using (StreamReader sr = File.OpenText(@"C:\Projects\InsuranceRight.Services\InsuranceRight.Services.AddressService\Json\addresses.json"))
            {
                JsonSerializer ser = new JsonSerializer();
                rootObject = (AddressJsonRootObject)ser.Deserialize(sr, typeof(AddressJsonRootObject));
                if (rootObject != null)
                {
                    foreach (Address address in rootObject.Addresses)
                    {
                        _addressList.Add(address);
                    }
                }
            }
            return _addressList;
        }

        public IEnumerable<string> GetValidZipCodes(IEnumerable<Address> ValidAddressList)
        {
            var list = new List<string>();
            foreach (Address address in ValidAddressList)
            {
                if (!list.Contains(address.ZipCode))
                {
                    list.Add(address.ZipCode);
                }
            }
            return list;
        }
    }
}
