using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.AddressService.Services.Data.Json;
using System;

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

            string addressFile = @"..\InsuranceRight.Services.AddressService\Services\Data\Json\addresses.json";
            var root = AppDomain.CurrentDomain.BaseDirectory;
            if (root.StartsWith("C:\\publish"))
            {
                addressFile = @"C:\publish\InsuranceRight\Services.Host\Services\Data\Json\addresses.json";
            }
            // must use relative path to Host project
            using (StreamReader sr = File.OpenText(addressFile))
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
