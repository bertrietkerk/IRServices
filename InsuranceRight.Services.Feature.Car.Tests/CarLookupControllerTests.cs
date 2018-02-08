using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
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

        private readonly CarLookupViewModel CorrectCarLookupViewModel = new CarLookupViewModel() { Brand = "Brand", Model = "Model", Edition = "Edition" };
        private readonly CarLookupViewModel IncorrectViewModel = new CarLookupViewModel() { Brand = "Incorrect", Model = "Incorrect", Edition = "Incorrect"};
        private readonly List<string> Brands = new List<string>() { "Brand1", "Brand2" };
        private readonly List<string> Models = new List<string>() { "Model1", "Model2" };
        private readonly List<string> Editions = new List<string>() { "Edition1", "Edition2" };
        private readonly decimal Weight = 1000m;
        private readonly decimal CatalogValue = 5000m;
        private readonly Dictionary<string, decimal> EditionDetails = new Dictionary<string, decimal>() { { "weight", 1000m }, { "catalogValue", 5000m } };
        private readonly string CorrectBrandString = "MINI";

        public CarLookupControllerTests()
        {
            Mock<ICarLookup> mockDataProvider = new Mock<ICarLookup>();
            mockDataProvider.Setup(x => x.GetBrands()).Returns(Brands);
            mockDataProvider.Setup(x => x.GetModels(CorrectBrandString)).Returns(Models);
            mockDataProvider.Setup(x => x.GetEditions(CorrectCarLookupViewModel.Brand, CorrectCarLookupViewModel.Model)).Returns(Editions);
            mockDataProvider.Setup(x => x.GetWeight(CorrectCarLookupViewModel.Brand, CorrectCarLookupViewModel.Model, CorrectCarLookupViewModel.Edition)).Returns(Weight);
            mockDataProvider.Setup(x => x.GetCatalogValue(CorrectCarLookupViewModel.Brand, CorrectCarLookupViewModel.Model, CorrectCarLookupViewModel.Edition)).Returns(CatalogValue);
            mockDataProvider.Setup(x => x.GetEditionDetails(CorrectCarLookupViewModel.Brand, CorrectCarLookupViewModel.Model, CorrectCarLookupViewModel.Edition)).Returns(EditionDetails);
            // more setup

            _controller = new CarLookupController(mockDataProvider.Object);
        }


        #region GetBrands test
        [TestMethod]
        public void GetBrands__ReturnsOkIncludingReturnObjectListOfBrands()
        {
            var expected = Brands;
            var result = _controller.GetBrands();
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(expected.Count, response.Object.Count);
            Assert.AreEqual(expected[0], response.Object[0]);
        }
        #endregion

        #region GetModels tests
        [TestMethod]
        public void GetModels__CorrectBrandString__ReturnsOkIncludingReturnObjectWithModels()
        {
            var expected = Models;
            var result = _controller.GetModels(CorrectBrandString);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(expected.Count, response.Object.Count);
            Assert.AreEqual(expected[0], response.Object[0]);
        }

        [TestMethod]
        public void GetModels__IncorrectString__ReturnsOkIncludingReturnObjectWithError()
        {
            var incorrectBrandString = "IncorrectBrandString";
            var result = _controller.GetModels(incorrectBrandString);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetModels__NullInput__ReturnsOkIncludingReturnObjectWithErrors()
        {
            var result = _controller.GetModels(null);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        #endregion

        #region GetEditions tests
        [TestMethod]
        public void GetEditions__CorrectCarLookupViewModel__ReturnsOkIncludingReturnObjectWithEditions()
        {
            var expected = Editions;
            var result = _controller.GetEditions(CorrectCarLookupViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(expected.Count, response.Object.Count);
            Assert.AreEqual(expected[0], response.Object[0]);
        }

        [TestMethod]
        public void GetEditions__EmptyViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            CarLookupViewModel nullModel = null;
            var result = _controller.GetEditions(nullModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetEditions__IncorrectViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var result = _controller.GetEditions(IncorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<string>>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        #endregion

        #region GetWeight tests
        [TestMethod]
        public void GetWeight__CorrectCarLookupViewModel__ReturnsOkIncludingReturnObjectWithWeight()
        {
            var expected = Weight;
            var result = _controller.GetWeight(CorrectCarLookupViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;

            Assert.IsNotNull(okResult);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(expected, response.Object);
        }

        [TestMethod]
        public void GetWeight__IncorrectViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var result = _controller.GetWeight(IncorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetWeight__EmptyViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            CarLookupViewModel nullModel = null;
            var result = _controller.GetWeight(nullModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }
        #endregion

        #region GetCatalogValue tests
        [TestMethod]
        public void GetCatalogValue__CorrectCarLookupViewModel__ReturnsOkIncludingReturnObjectWithCatalogValue()
        {
            var expected = CatalogValue;
            var result = _controller.GetCatalogValue(CorrectCarLookupViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;

            Assert.IsNotNull(okResult);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(expected, response.Object);
        }

        [TestMethod]
        public void GetCatalogValue__IncorrectViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var result = _controller.GetCatalogValue(IncorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetCatalogValue__NullViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            CarLookupViewModel nullViewModel = null;
            var result = _controller.GetCatalogValue(nullViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<decimal>;

            Assert.IsNotNull(okResult);
            Assert.IsTrue(response.HasErrors);
        }

        #endregion

        #region GetEditionDetails tests

        [TestMethod]
        public void GetEditionDetails__CarLookupViewModelWithBrandModelEdition__ReturnsOkIncludingReturnObjectWithDictIncludingCatalogValueAndWeight()
        {
            var result = _controller.GetEditionDetails(CorrectCarLookupViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<Dictionary<string, decimal>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.AreEqual(EditionDetails.Count, response.Object.Count);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(EditionDetails["weight"], response.Object["weight"]);
        }

        #endregion
    }
}
