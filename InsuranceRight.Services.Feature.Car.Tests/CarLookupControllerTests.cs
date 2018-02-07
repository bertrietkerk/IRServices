using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Feature.Car.Services.Data;
using InsuranceRight.Services.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarLookupControllerTests
    {
        private readonly CarLookupController _controller;

        private readonly CarLookupViewModel MockCarViewModel = new CarLookupViewModel() { Brand = "Brand", Model = "Model", Edition = "Edition" };
        private readonly List<string> Brands = new List<string>() { "Brand1", "Brand2" };
        private readonly List<string> Models = new List<string>() { "Model1", "Model2" };
        private readonly List<string> Editions = new List<string>() { "Edition1", "Edition2" };
        private readonly decimal Weight = 1000m;
        private readonly decimal CatalogValue = 5000m;
        private readonly Dictionary<string, decimal> EditionDetails = new Dictionary<string, decimal>() { { "weight", 1000m }, { "catalogValue", 5000m } };

        public CarLookupControllerTests()
        {
            Mock<ICarLookup> mockDataProvider = new Mock<ICarLookup>();
            mockDataProvider.Setup(x => x.GetBrands()).Returns(Brands);
            mockDataProvider.Setup(x => x.GetModels(It.IsAny<string>())).Returns(Models);
            mockDataProvider.Setup(x => x.GetEditions(It.IsAny<string>(), It.IsAny<string>())).Returns(Editions);
            mockDataProvider.Setup(x => x.GetWeight(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Weight);
            mockDataProvider.Setup(x => x.GetCatalogValue(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(CatalogValue);
            mockDataProvider.Setup(x => x.GetEditionDetails(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(EditionDetails);
            // more setup

            _controller = new CarLookupController(mockDataProvider.Object);
        }

        [TestMethod]
        public void GetBrands__ReturnsOkIncludingReturnObjectListOfBrands()
        {
            var expected = Brands;
            var result = _controller.GetBrands();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;
            var actual = response.Object;

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        public void GetModels__AnyStringBrand__ReturnsOkIncludingReturnObjectWithModels()
        {
            var expected = Models;
            var result = _controller.GetModels("MINI");
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;
            var actual = response.Object;

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        public void GetEditions__CarLookupViewModelWithBrandModel__ReturnsOkIncludingReturnObjectWithEditions()
        {
            var expected = Editions;
            var result = _controller.GetEditions(MockCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;
            var actual = response.Object;

            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0], actual[0]);
        }

        [TestMethod]
        public void GetWeight__CarLookupViewModelWithBrandModelEdition__ReturnsOkIncludingReturnObjectWithWeight()
        {
            var expected = Weight;
            var result = _controller.GetWeight(MockCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;
            var actual = response.Object;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCatalogValue__CarLookupViewModelWithBrandModelEdition__ReturnsOkIncludingReturnObjectWithCatalogValue()
        {
            var expected = CatalogValue;
            var result = _controller.GetCatalogValue(MockCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;
            var actual = response.Object;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetEditionDetails__CarLookupViewModelWithBrandModelEdition__ReturnsOkIncludingReturnObjectWithDictIncludingCatalogValueAndWeight()
        {
            var result = _controller.GetEditionDetails(MockCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<Dictionary<string, decimal>>;
            var actual = response.Object;

            Assert.IsNotNull(actual);
            Assert.AreEqual(EditionDetails.Count, actual.Count);
            Assert.AreEqual(EditionDetails["weight"], actual["weight"]);
        }
    }
}
