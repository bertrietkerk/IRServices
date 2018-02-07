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

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarPolicyDocumentControllerTests
    {
        private CarPolicyDocumentController _controller;

        public CarPolicyDocumentControllerTests()
        {
            Mock<ICarDocumentService> mockDocumentService = new Mock<ICarDocumentService>();
            mockDocumentService.Setup(x => x.GetDocuments(It.IsAny<CarViewModel>())).Returns(new List<PolicyDocument>() { new PolicyDocument() { }, new PolicyDocument() { } });

            _controller = new CarPolicyDocumentController(mockDocumentService.Object);
        }

        [TestMethod]
        public void Documents__ValidCarViewModel__ReturnsOkIncludingReturnObjectListPolicyDocuments()
        {
            var mockCarViewModel = new CarViewModel()
            {
                Payment = new CarPayment() { },
                PremiumFactors = new CarPremiumFactors()
                {
                    Car = new CarObject() { Price = new CarPrice() { } },
                    Driver = new MostFrequentDriverViewModel() { ResidenceAddress = new Address() { } }
                }
            };

            var result = _controller.Documents(mockCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<IEnumerable<PolicyDocument>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(response);
        }
    }
}
