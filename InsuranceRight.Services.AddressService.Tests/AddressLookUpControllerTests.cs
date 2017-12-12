using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceRight.Services.AddressService.Controllers;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.AddressService.Repositories;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Models.Response;

namespace InsuranceRight.Services.AddressService.Tests
{
    [TestClass]
    public class AddressLookupControllerTests
    {
        //private readonly IAddressCheck _addressChecker;
        private readonly AddressLookupController _controller;

        public AddressLookupControllerTests()
        {
            var addressChecker = new DefaultAddressLookup(new MockDataProvider());
            _controller = new AddressLookupController(addressChecker);
        }

        [DataTestMethod]
        [DataRow("1111AA", false, "")]
        [DataRow("2222 bb", false, "Zipcode was found!")]
        [DataRow(null, true, "Zipcode was null or empty")]
        [DataRow("", true, "Zipcode was null or empty")]
        [DataRow("      ", true, "Zipcode was null or empty")]
        [DataRow("AA1111", true, "Zipcode wasn't in the correct format (e.g. 1111AA)")]
        [DataRow("hello!", true, "Zipcode wasn't in the correct format (e.g. 1111AA)")]
        [DataRow("9999ZZ", true, "Zipcode was not found")]
        public void ValidateZipCode__ValidAndInvalidZipcodes__ReturnsBoolAndMessage(string inputZipcode, bool expectedHasErrors, string expectedMessage)
        {
            ZipCode zipcode = new ZipCode() { Zipcode = inputZipcode };

            var result = _controller.ValidateZipcode(zipcode) as OkObjectResult;
            var response = (ReturnObject<ZipCode>)result.Value;

            var actualHasErrors = response.HasErrors;
            if (response.ErrorMessages.Count > 0)
            {
                var actualMessage = response.ErrorMessages[0];
                Assert.AreEqual(expectedMessage, actualMessage);
            }
            
            Assert.AreEqual(expectedHasErrors, actualHasErrors);
            
        }
        
        [DataTestMethod]
        [DataRow("1111AA", "1", "a", false, "")]
        [DataRow("9999ZZ", "99", "a", true, "No combination was found for 9999ZZ 99A")]
        [DataRow("9999ZZ", "", "", true, "'Zipcode' and/or 'housenumber' of address cannot be null or empty")]
        [DataRow(null, "", "", true, "'Zipcode' and/or 'housenumber' of address cannot be null or empty")]
        public void GetFullAddress__ValidAndInvalidInput__ReturnsBoolAndMessage(string zipCode, string houseNumber, string houseNumberExtension, bool expectedHasErrors, string expectedMessage)
        {
            Address incompleteAddress = new Address(zipCode, houseNumber, houseNumberExtension );

            var result = _controller.GetFullAddress(incompleteAddress) as OkObjectResult;
            var response = (ReturnObject<Address>)result.Value;
            var actualHasErrors = response.HasErrors;
            if (response.ErrorMessages.Count > 0)
            {
                var actualMessage = response.ErrorMessages[0];
                Assert.AreEqual(expectedMessage, actualMessage);
            }
            
            Assert.AreEqual(expectedHasErrors, actualHasErrors);
        }
    }
}
