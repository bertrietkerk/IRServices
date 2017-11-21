using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRight.Services.AddressService.Models;
using System.Collections.Generic;
using System;
using InsuranceRight.Services.AddressService.Controllers;
using InsuranceRight.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceRight.Services.AddressService.Tests
{
    [TestClass]
    public class AddressCheckControllerTests
    {
        //private readonly IAddressCheck _addressChecker;
        private readonly AddressCheckController _controller;

        public AddressCheckControllerTests()
        {
            var addressChecker = new AddressCheckRepository(new MockDataProvider());
            _controller = new AddressCheckController(addressChecker);
        }

        [DataTestMethod]
        [DataRow("1111AA", true, "Zipcode was found!")]
        [DataRow("2222 bb", true, "Zipcode was found!")]
        [DataRow(null, false, "Zipcode was null or empty")]
        [DataRow("", false, "Zipcode was null or empty")]
        [DataRow("      ", false, "Zipcode was null or empty")]
        [DataRow("AA1111", false, "Zipcode wasn't in the correct format (e.g. 1111AA)")]
        [DataRow("hello!", false, "Zipcode wasn't in the correct format (e.g. 1111AA)")]
        [DataRow("9999ZZ", false, "Zipcode was not found")]
        public void ValidateZipCode__ValidAndInvalidZipcodes__ReturnsBoolAndMessage(string inputZipcode, bool expectedIsValid, string expectedMessage)
        {
            ZipCode zipcode = new ZipCode() { Zipcode = inputZipcode };

            var result = _controller.ValidateZipcode(zipcode) as OkObjectResult;
            var response = (ReturnObject<ZipCode>)result.Value;
            var actualIsValid = response.IsValid;
            var actualMessage = response.Message;

            Assert.AreEqual(expectedIsValid, actualIsValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
        
        [DataTestMethod]
        [DataRow("1111AA", "1", "a", true, "Address found!")]
        [DataRow("9999ZZ", "99", "a", false, "No combination was found for 9999ZZ 99A")]
        public void GetFullAddress__ValidAndInvalidInput__ReturnsBoolAndMessage(string zipCode, string houseNumber, string houseNumberExtension, bool expectedIsValid, string expectedMessage)
        {
            Address incompleteAddress = new Address(zipCode, houseNumber, houseNumberExtension );

            var result = _controller.GetFullAddress(incompleteAddress) as OkObjectResult;
            var response = (ReturnObject<Address>)result.Value;
            var actualIsValid = response.IsValid;
            var actualMessage = response.Message;

            Assert.AreEqual(expectedIsValid, actualIsValid);
            Assert.AreEqual(expectedMessage, actualMessage);
        }



        //[TestMethod]
        //public void GetFullAddressBy_ZipCodeAndHouseNumber()
        //{
        //    var zipCode = "2222BB";
        //    var houseNumber = "2";
        //    var fullAddress = _addressChecker.GetFullAddress(zipCode, houseNumber);

        //    Assert.IsNotNull(fullAddress);
        //    Assert.IsNotNull(fullAddress.Street);
        //    Assert.IsNotNull(fullAddress.City);
        //    Assert.IsNotNull(fullAddress.Country);

        //    Assert.AreEqual(zipCode, fullAddress.ZipCode);
        //    Assert.AreEqual(houseNumber, fullAddress.HouseNumber);
        //}

        //[DataTestMethod]
        //[DataRow("1111AA", "1", "A")]
        //[DataRow("2222BB", "2", "")]
        //public void GetFullAddressBy_ZipCodeAndHouseNumberAndHouseNumberExtension(string zipCode, string houseNumber, string houseNumberExt)
        //{
        //    var fullAddress = _addressChecker.GetFullAddress(zipCode, houseNumber, houseNumberExt);
        //    Assert.IsNotNull(fullAddress);
        //    Assert.IsNotNull(fullAddress.Street);
        //    Assert.IsNotNull(fullAddress.City);
        //    Assert.IsNotNull(fullAddress.Country);

        //    Assert.AreEqual(zipCode, fullAddress.ZipCode);
        //    Assert.AreEqual(houseNumber, fullAddress.HouseNumber);
        //    Assert.AreEqual(houseNumberExt.ToUpper(), fullAddress.HouseNumberExtension);
        //}

        //[DataTestMethod]
        //[DataRow("2222BB", "")]
        //[DataRow("", "2")]
        //public void GetFullAddressBy_EmptyParameters_ThrowsNullReferenceException(string zipCode, string houseNumber)
        //{
        //    Assert.ThrowsException<NullReferenceException>(() => _addressChecker.GetFullAddress(zipCode, houseNumber));
        //}



    }
}
