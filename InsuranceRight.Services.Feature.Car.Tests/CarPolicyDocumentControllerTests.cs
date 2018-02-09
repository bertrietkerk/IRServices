using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarPolicyDocumentControllerTests
    {
        private readonly CarPolicyDocumentController _controller;
        private readonly CarViewModel CorrectViewModel;

        public CarPolicyDocumentControllerTests()
        {
            CorrectViewModel = new CarViewModel() { Payment = new CarPayment() { }, PremiumFactors = new CarPremiumFactors() { Car = new CarObject() { Price = new CarPrice() { } }, Driver = new MostFrequentDriverViewModel() { ResidenceAddress = new Address() { } } } };

            Mock<ICarDocumentService> mockDocumentService = new Mock<ICarDocumentService>();
            mockDocumentService.Setup(x => x.GetDocuments(CorrectViewModel)).Returns(new List<PolicyDocument>() { new PolicyDocument() { }, new PolicyDocument() { } });

            _controller = new CarPolicyDocumentController(mockDocumentService.Object);
        }

        [TestMethod]
        public void Documents__CorrectCarViewModel__ReturnsOkIncludingReturnObjectListPolicyDocuments()
        {
            var result = _controller.Documents(CorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<PolicyDocument>>;
            var responseList = response.Object;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
            Assert.IsNotNull(responseList);
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(responseList.Count == 2);
        }

        [TestMethod]
        public void Documents__IncorrectCarViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var incorrectViewModel = new CarViewModel() { };
            var result = _controller.Documents(incorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<PolicyDocument>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void Documents__NullCarViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            CarViewModel nullViewModel = null;
            var result = _controller.Documents(nullViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<PolicyDocument>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }
    }
}
