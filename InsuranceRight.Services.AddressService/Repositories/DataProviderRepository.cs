using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.Models.Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace InsuranceRight.Services.AddressService.Repositories
{
    public class DataProviderRepository : IDataProvider
    {
        public IEnumerable<Address> GetValidAddresses()
        {
            var list = new List<Address>();
            Address address;
            //string[] files = Directory.GetFiles(@".\Json");
            string[] files = Directory.GetFiles(@"C:\Projects\InsuranceRight.Services\InsuranceRight.Services.AddressService\Json");
            foreach (String file in files)
            {
                using (StreamReader sr = File.OpenText(file))
                {
                    JsonSerializer ser = new JsonSerializer();
                    address = (Address)ser.Deserialize(sr, typeof(Address));
                    if (address != null)
                    {
                        list.Add(address);
                    }
                }
            }
            return list;
        }


        public IEnumerable<String> GetValidZipCodes(IEnumerable<Address> ValidAddressList)
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
