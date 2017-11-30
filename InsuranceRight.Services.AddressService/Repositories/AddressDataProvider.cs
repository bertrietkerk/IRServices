using InsuranceRight.Services.AddressService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Shared.Models;
using System.IO;
using Newtonsoft.Json;
using InsuranceRight.Services.AddressService.Json;

namespace InsuranceRight.Services.AddressService.Repositories
{
    public class AddressDataProvider : IDataProvider
    {
        List<Address> _addressList;

        public AddressDataProvider()
        {
            _addressList = new List<Address>();
        }

        public IEnumerable<Address> GetValidAddresses()
        {
            AddressJsonRootObject rootObject;
            using (StreamReader sr = File.OpenText(@".\Json\addresses.json"))
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
