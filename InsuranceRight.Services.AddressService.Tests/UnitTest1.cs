using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRight.Services.AddressService.Models;
using System.Collections.Generic;
using System;

namespace InsuranceRight.Services.AddressService.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IAddressCheck _addressChecker;

        public UnitTest1()
        {
            var dataProvider = new MockDataProvider();
            _addressChecker = new AddressCheckRepository(dataProvider);
        }
        

        [DataTestMethod]
        [DataRow("1111AA")]
        [DataRow("2222bb")]
        public void ValidateValidZipCodesToUpperAndToLower(string zipcode)
        {
            var isValid = _addressChecker.IsZipCodeValid(zipcode);
            Assert.IsTrue(isValid);
        }

        [DataTestMethod]
        [DataRow("1122AA")]
        [DataRow(null)]
        [DataRow("")]
        public void ValidateInvalidZipCodes(string zipcode)
        {
            Assert.ThrowsException<NullReferenceException>(() => _addressChecker.IsZipCodeValid(zipcode));
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
            Assert.IsNotNull(fullAddress.Street);
            Assert.IsNotNull(fullAddress.City);
            Assert.IsNotNull(fullAddress.Country);

            Assert.AreEqual(zipCode, fullAddress.ZipCode);
            Assert.AreEqual(houseNumber, fullAddress.HouseNumber);
            Assert.AreEqual(houseNumberExt.ToUpper(), fullAddress.HouseNumberExtension);
        }

        [DataTestMethod]
        [DataRow("2222BB", "")]
        [DataRow("", "2")]
        public void GetFullAddressBy_EmptyParameters_ThrowsNullReferenceException(string zipCode, string houseNumber)
        {
            Assert.ThrowsException<NullReferenceException>(() => _addressChecker.GetFullAddress(zipCode, houseNumber));
        }



    }
}
