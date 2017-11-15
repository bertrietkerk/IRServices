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
            _addressChecker = new AddressCheck(dataProvider);
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
        [DataRow("null")]
        [DataRow("")]
        public void ValidateInvalidZipCodes(string zipcode)
        {
            Assert.IsFalse(_addressChecker.IsZipCodeValid(zipcode));
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
            Assert.AreEqual(houseNumberExt.ToLower(), fullAddress.HouseNumberExtension);
        }

        [DataTestMethod]
        [DataRow("2222BB", "")]
        [DataRow("", "2")]
        public void GetFullAddressBy_EmptyParameters_ThrowsNullReferenceException(string zipCode, string houseNumber)
        {
            Assert.ThrowsException<NullReferenceException>(() => _addressChecker.GetFullAddress(zipCode, houseNumber));
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
            Assert.IsNotNull(fullAddress.Street);
            Assert.IsNotNull(fullAddress.City);
            Assert.IsNotNull(fullAddress.Country);

            Assert.AreEqual(fullAddress.ZipCode, incompleteAddress.ZipCode);
            Assert.AreEqual(fullAddress.HouseNumber, incompleteAddress.HouseNumber);
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

        [TestMethod]
        public void GetFullAddressBy_NullAddress_ThrowsNullReferenceException()
        {
            Address nullAddress = null;

            Assert.ThrowsException<NullReferenceException>(() => _addressChecker.GetFullAddress(nullAddress));
        }

        [TestMethod]
        public void GetFullAddressBy_AddressWithNullZipCode_ThrowsNullReferenceException()
        {
            var inValidAddress = new Address()
            {
                ZipCode = "",
                HouseNumber = null
            };
            Assert.ThrowsException<NullReferenceException>(() => _addressChecker.GetFullAddress(inValidAddress));
        }

        

    }
}
