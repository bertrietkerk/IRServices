using InsuranceRight.Services.AddressService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.AddressService.Tests
{
    class MockDataProvider : IDataProvider
    {
        public IEnumerable<Address> GetValidAddresses()
        {
            return new List<Address>()
            {
                new Address()
                {
                    ZipCode = "1111AA",
                    HouseNumber = "1",
                    HouseNumberExtension = "a",
                    Street = "A-laan",
                    City = "Almere",
                    Country = "Holland"
                },
                new Address()
                {
                    ZipCode = "2222BB",
                    HouseNumber = "2",
                    HouseNumberExtension = "",
                    Street = "B-hof",
                    City = "Beverwijk",
                    Country = "Holland"
                }
            };
        }

        public IEnumerable<string> GetValidZipCodes(IEnumerable<Address> ValidAddressList)
        {
            return new List<string>()
            {
                "1111AA", "2222BB"
            };
        }
    }
}
