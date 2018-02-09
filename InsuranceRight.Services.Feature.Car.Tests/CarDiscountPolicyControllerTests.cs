using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarDiscountPolicyControllerTests
    {
        private readonly CarDiscountPolicyController _controller;
        private readonly string CorrectDiscountCode = "InsuranceRight";
        private readonly string IncorrectCode = "Incorrect";
        private readonly CarDiscountPolicy CorrectDiscountPolicy = new CarDiscountPolicy() { IsDiscountFound = true, Amount = 1 };
        private readonly CarDiscountPolicy IncorrectDiscountPolicy = new CarDiscountPolicy() { IsDiscountFound = false, Amount = 0 };


        public CarDiscountPolicyControllerTests()
        {
            Mock<ICarDiscountPolicy> mockDataProvider = new Mock<ICarDiscountPolicy>();
            mockDataProvider.Setup(x => x.GetDiscountForGroup(It.Is<string>(str => str != CorrectDiscountCode))).Returns(IncorrectDiscountPolicy);
            mockDataProvider.Setup(x => x.GetDiscountForGroup(CorrectDiscountCode)).Returns(CorrectDiscountPolicy);
            _controller = new CarDiscountPolicyController(mockDataProvider.Object);
        }

        [TestMethod]
        public void GetDiscountForGroup__CorrectCode_ReturnsOkInclReturnObjectWithCarDiscountPolicy()
        {
            var result = _controller.GroupDiscounts(CorrectDiscountCode);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(response.Object.IsDiscountFound);
            Assert.AreEqual(CorrectDiscountPolicy.Amount, response.Object.Amount);
        }

        [TestMethod]
        public void GetDiscountForGroup__IncorrectCode_ReturnsOkInclReturnObjectWithError()
        {
            var result = _controller.GroupDiscounts(IncorrectCode);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.IsFalse(response.Object.IsDiscountFound);
        }

        [TestMethod]
        public void GetDiscountForGroup__NullInput_ReturnsOkInclReturnObjectWithError()
        {
            string nullString = null;
            var result = _controller.GroupDiscounts(nullString);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.IsFalse(response.Object.IsDiscountFound);
        }

    }
}
