using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRight.Services.AddressService.Models;
using System.Collections.Generic;

namespace InsuranceRight.Services.AddressService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly AddressCheck _addressChecker = new AddressCheck();
//        private readonly List<string> ValidZipCodes = new List<string>() { "1111AA", "2222BB", "3333CC", "4444DD", "5555EE" };


        [DataTestMethod]
        [DataRow("1111AA")]
        [DataRow("2222bb")]
        public void ValidateValidAddressesToUpperAndToLower(string address)
        {
            var isValid = _addressChecker.ValidateZipCode(address);
            Assert.IsTrue(isValid);
        }

        [DataTestMethod]
        [DataRow("1122AA")]
        [DataRow("null")]
        public void ValidateInvalidAddresses(string address)
        {
            Assert.IsFalse(_addressChecker.ValidateZipCode(address));
        }

        [TestMethod]
        public void GetFullAddressBy_ZipCodeAndHouseNumber()
        {
            var zipCode = "2222BB";
            var houseNumber = "2";
            var fullAddress = _addressChecker.GetFullAddress(zipCode, houseNumber);

            Assert.IsNotNull(fullAddress);
            Assert.IsNotNull(fullAddress.Street);
            Assert.IsNotNull(fullAddress.City);
            Assert.IsNotNull(fullAddress.Country);

            Assert.AreEqual(zipCode, fullAddress.ZipCode);
            Assert.AreEqual(houseNumber, fullAddress.HouseNumber);
        }

        [DataTestMethod]
        [DataRow("1111AA", "1", "A")]
        [DataRow("2222BB", "2", "")]
        public void GetFullAddressBy_ZipCodeAndHouseNumberAndHouseNumberExtension(string zipCode, string houseNumber, string houseNumberExt)
        {
            var fullAddress = _addressChecker.GetFullAddress(zipCode, houseNumber, houseNumberExt);
            Assert.IsNotNull(fullAddress);
        }
        
        [TestMethod]
        public void GetFullAddressBy_IncompleteValidAddress()
        {
            var incompleteAddress = new Address()
            {
                ZipCode = "2222BB",
                HouseNumber = "2"
            };
            var fullAddress = _addressChecker.GetFullAddress(incompleteAddress);
            Assert.IsNotNull(fullAddress);
        }

        [TestMethod]
        public void GetFullAddressBy_IncompleteInValidAddress()
        {
            var incompleteInvalidAddress = new Address()
            {
                ZipCode = "2222BB",
                HouseNumber = "91230912309123"
            };
            var fullAddress = _addressChecker.GetFullAddress(incompleteInvalidAddress);
            Assert.IsNull(fullAddress);
        }
    }
}
