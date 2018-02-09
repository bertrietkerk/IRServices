using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarAcceptanceControllerTests
    {
        private readonly AcceptanceController _controller;
        private readonly CarViewModel CorrectCarViewModel;

        public CarAcceptanceControllerTests()
        {
            CorrectCarViewModel = new CarViewModel() { PremiumFactors = new CarPremiumFactors() { Car = new CarObject() { Price = new CarPrice() { CatalogPrice = 5000 } }, Driver = new MostFrequentDriverViewModel() { } } };

            // Mock ICarAcceptance
            Mock<ICarAcceptance> mockAcceptance = new Mock<ICarAcceptance>();
            mockAcceptance.Setup(x => x.Check(It.IsAny<MostFrequentDriverViewModel>(), It.IsAny<CarObject>())).Returns(new AcceptanceStatus() { IsAccepted = true });
            _controller = new AcceptanceController(mockAcceptance.Object, Options.Create(new AcceptanceSettings()));
        }


        [TestMethod]
        public void Check__ValidDriverViewModel__ReturnsOkIncludingReturnObjectAcceptanceStatus()
        {
            var result = _controller.Check(CorrectCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<AcceptanceStatus>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Object.IsAccepted);
        }


        [TestMethod]
        public void Check__NullViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var okResult = _controller.Check(null) as OkObjectResult;
            var response = okResult.Value as ReturnObject<AcceptanceStatus>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }
    }
}
