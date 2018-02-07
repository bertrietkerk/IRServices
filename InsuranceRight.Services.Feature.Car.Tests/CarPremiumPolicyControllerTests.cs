using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Feature.Car.Models.Enums;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Models.Enums;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarPremiumPolicyControllerTests
    {
        private readonly CarPremiumPolicyController sut_controller;
        private CarViewModel baseCarViewModel;


        public CarPremiumPolicyControllerTests()
        {
            // Mock ICarPremiumPolicy
            Mock<ICarPremiumPolicy> mockPremiumPolicy = new Mock<ICarPremiumPolicy>();
            mockPremiumPolicy.Setup(x => x.GetPaymentFrequencyDiscount(It.IsAny<int>())).Returns(99m);
            mockPremiumPolicy.Setup(x => x.GetDiscountForGroup(It.IsAny<string>())).Returns(new CarDiscountPolicy() { Amount = 1, IsDiscountFound = true });
            mockPremiumPolicy.Setup(x => x.GetVariants(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<KilometersPerYear>())).Returns(new List<ProductVariant>() { new ProductVariant() { Premium = 50 } });
            
            //setup other premiumCalculations

            // Mock ICarAcceptance
            Mock<ICarAcceptance> mockAcceptance = new Mock<ICarAcceptance>();
            mockAcceptance.Setup(x => x.Check(It.IsAny<MostFrequentDriverViewModel>(), It.IsAny<CarObject>())).Returns(new AcceptanceStatus() { IsAccepted = true });

            sut_controller = new CarPremiumPolicyController(
                mockPremiumPolicy.Object, 
                Options.Create(new PremiumCalculationSettings()), 
                mockAcceptance.Object
            );

            baseCarViewModel = new CarViewModel();

        }


        [TestMethod]
        public void GroupCodeDiscount__AnyString__ReturnsOkIncludingReturnObjectCarDiscountPolicy()
        {
            var result = sut_controller.GroupCodeDiscount("hello");
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Object.Amount);
        }

        [TestMethod]
        public void PaymentFrequencyDiscount__AnyValidInput__ReturnsOkIncludingReturnObject()
        {
            baseCarViewModel.Payment = new CarPayment() { PaymentFrequency = PaymentFrequency.Annual };

            var result = sut_controller.PaymentFrequencyDiscount(baseCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<PaymentFrequencyDiscountModel>;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(response);
            Assert.AreEqual(99m, response.Object.Amount);
        }

        [TestMethod]
        public void GetVariants__ValidCarViewModel__ReturnsOkIncludingReturnObject()
        {
            
        }

        [TestMethod]
        public void GetCoverages__ValidCarViewModel__ReturnsOkIncludingReturnObject()
        {

        }

        [TestMethod]
        public void GetVariantsAndCoverages__ValidCarViewModel__ReturnsOkIncludingReturnObject()
        {

        }
    }
}
