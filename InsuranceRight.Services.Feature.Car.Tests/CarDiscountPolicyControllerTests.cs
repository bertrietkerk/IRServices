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
    public class CarDiscountPolicyControllerTests
    {
        [TestMethod]
        public void GetDiscountForGroup__AnyStringInput_ReturnsCarDiscountPolicy()
        {
             
            Mock<ICarDiscountPolicy> mockDataProvider = new Mock<ICarDiscountPolicy>();
            mockDataProvider.Setup(x => x.GetDiscountForGroup(It.IsAny<string>())).Returns(new CarDiscountPolicy() { Amount = 1 });
            var sut_controller = new CarDiscountPolicyController(mockDataProvider.Object);

            var result = sut_controller.GroupDiscounts("hello");
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            
        }
    }
}
