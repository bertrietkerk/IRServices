using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class LicensePlateLookupControllerTests
    {
        private readonly LicensePlateLookupController _controller;

        public LicensePlateLookupControllerTests()
        {
            Mock<ILicensePlateLookup> mockLicensePlateLookup = new Mock<ILicensePlateLookup>();
            mockLicensePlateLookup.Setup(x => x.GetCar(It.IsAny<string>())).Returns(new CarObject() { });

            _controller = new LicensePlateLookupController(mockLicensePlateLookup.Object);
        }

        [TestMethod]
        public void GetCarDetails__Input__ReturnsOkWithReturnObjectCarObject()
        {
            var result = _controller.GetCarDetails("BR-14-IR");
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarObject>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
        }
    }
}
