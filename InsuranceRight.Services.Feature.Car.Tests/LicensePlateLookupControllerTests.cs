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
        private readonly string Licenseplate;

        public LicensePlateLookupControllerTests()
        {
            Licenseplate = "TE-ST-01";

            Mock<ILicensePlateLookup> mockLicensePlateLookup = new Mock<ILicensePlateLookup>();
            mockLicensePlateLookup.Setup(x => x.GetCar(Licenseplate)).Returns(new CarObject() { });

            _controller = new LicensePlateLookupController(mockLicensePlateLookup.Object);
        }

        [TestMethod]
        public void GetCarDetails__CorrectLicenseplate__ReturnsOkWithReturnObjectCarObject()
        {
            var okResult = _controller.GetCarDetails(Licenseplate) as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarObject>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Object);
        }

        [TestMethod]
        public void GetCarDetails__IncorrectLicenseplate__ReturnsOkWithReturnObjectError()
        {
            var okResult = _controller.GetCarDetails("incorrect licenseplate") as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarObject>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetCarDetails__NullInput__ReturnsOkWithReturnObjectError()
        {
            var okResult = _controller.GetCarDetails(null) as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarObject>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

    }
}
