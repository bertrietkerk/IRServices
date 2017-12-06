using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.Models.Foundation;
using System.Collections.Generic;

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
                    HouseNumberExtension = "A",
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
