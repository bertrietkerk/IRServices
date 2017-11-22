using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRight.Services.AddressService.Controllers;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.AddressService.Repositories;
using InsuranceRight.Services.Shared.Models;

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
        [DataRow("9999ZZ", "", "", false, "'Zipcode' and/or 'housenumber' of address cannot be null or empty")]
        [DataRow(null, "", "", false, "'Zipcode' and/or 'housenumber' of address cannot be null or empty")]
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
    }
}
