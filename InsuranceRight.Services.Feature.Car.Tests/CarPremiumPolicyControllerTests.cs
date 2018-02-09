using InsuranceRight.Services.Feature.Car.Controllers;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Feature.Car.Models.Enums;
using InsuranceRight.Services.Feature.Car.Models.Response;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Enums;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarPremiumPolicyControllerTests
    {
        private readonly CarPremiumPolicyController _controller;
        private readonly CarViewModel CorrectViewModel;
        private readonly CarViewModel YoungDriverCarViewModel;
        private readonly string CorrectGroupCode;
        private readonly string Licenseplate;
        private readonly DateTime Birthdate;
        private readonly string ClaimfreeYear;
        private readonly string Zipcode;

        public CarPremiumPolicyControllerTests()
        {
            CorrectGroupCode = "InsuranceRight";
            Licenseplate = "TE-ST-01";
            Birthdate = new DateTime(1993, 1, 1);
            ClaimfreeYear = "1";
            Zipcode = "1111AA";
            CorrectViewModel = new CarViewModel()
            {
                Payment = new CarPayment() { PaymentFrequency = PaymentFrequency.Annual },
                PremiumFactors = new CarPremiumFactors()
                {
                    GroupDiscountCode = CorrectGroupCode,
                    Car = new CarObject()
                    {
                        LicensePlate = Licenseplate,
                    },
                    Driver = new MostFrequentDriverViewModel()
                    {
                        ResidenceAddress = new Address()
                        {
                            ZipCode = Zipcode
                        },
                        ZipCode = Zipcode,
                        BirthDate = new DateTime(1993, 1, 1),
                        KilometersPerYear = KilometersPerYear.Between10000and15000,
                        DamageFreeYears = ClaimfreeYear
                    }
                }
            };
            YoungDriverCarViewModel = new CarViewModel()
            {
                Payment = new CarPayment() { PaymentFrequency = PaymentFrequency.Annual },
                PremiumFactors = new CarPremiumFactors()
                {
                    GroupDiscountCode = CorrectGroupCode,
                    Car = new CarObject()
                    {
                        LicensePlate = Licenseplate,
                    },
                    Driver = new MostFrequentDriverViewModel()
                    {
                        ResidenceAddress = new Address()
                        {
                            ZipCode = Zipcode
                        },
                        ZipCode = Zipcode,
                        BirthDate = DateTime.Now,
                        KilometersPerYear = KilometersPerYear.Between10000and15000,
                        DamageFreeYears = ClaimfreeYear
                    }
                }
            }; ;

            // Mock setup
            Mock<ICarPremiumPolicy> mockPremiumPolicy = new Mock<ICarPremiumPolicy>();
            // paymentfreq
            mockPremiumPolicy.Setup(x => x.GetPaymentFrequencyDiscount(It.IsAny<int>())).Returns(99m);
            // groupcode
            mockPremiumPolicy.Setup(x => x.GetDiscountForGroup(CorrectGroupCode))
                .Returns(new CarDiscountPolicy() { Amount = 1, IsDiscountFound = true });
            mockPremiumPolicy.Setup(x => x.GetDiscountForGroup(It.Is<string>(str => str != CorrectGroupCode)))
                .Returns(new CarDiscountPolicy() { IsDiscountFound = false });
            // getvariants
            mockPremiumPolicy.Setup(x => x.GetVariants(Licenseplate, Birthdate, ClaimfreeYear, Zipcode, It.IsAny<KilometersPerYear>(), CorrectGroupCode, CorrectViewModel.Payment.PaymentFrequency))
                .Returns(new List<ProductVariant>() { new ProductVariant() { Premium = 50 } });
            // getcoverages
            mockPremiumPolicy.Setup(x => x.GetCoverages(Licenseplate, Birthdate, ClaimfreeYear, Zipcode))
                .Returns(new List<Coverage>() { new Coverage() { Premium = 5 } });
            // getvariantsandcoverages
            mockPremiumPolicy.Setup(x => x.GetVariantsAndCoverages(Licenseplate, Birthdate, ClaimfreeYear, Zipcode, It.IsAny<KilometersPerYear>()))
                .Returns(new VariantsAndCoverages() { Coverages = new List<Coverage>(), Variants = new List<ProductVariant>() });

            // Mock ICarAcceptance
            Mock<ICarAcceptance> mockAcceptance = new Mock<ICarAcceptance>();
            mockAcceptance.Setup(x => x.Check(CorrectViewModel.PremiumFactors.Driver, CorrectViewModel.PremiumFactors.Car)).Returns(new AcceptanceStatus() { IsAccepted = true });
            mockAcceptance.Setup(x => x.Check(YoungDriverCarViewModel.PremiumFactors.Driver, YoungDriverCarViewModel.PremiumFactors.Car)).Returns(new AcceptanceStatus() { IsAccepted = false });

            // Controller setup
            _controller = new CarPremiumPolicyController(mockPremiumPolicy.Object, Options.Create(new PremiumCalculationSettings() { IncludeAcceptanceCheck = true }), mockAcceptance.Object);
        }

        #region GroupCodeDiscount tests
        [TestMethod]
        public void GroupCodeDiscount__CorrectGroupCode__ReturnsOkIncludingReturnObjectCarDiscountPolicy()
        {
            var result = _controller.GroupCodeDiscount(CorrectGroupCode);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(1, response.Object.Amount);
            Assert.IsTrue(response.Object.IsDiscountFound);
        }

        [TestMethod]
        public void GroupCodeDiscount__IncorrectGroupCode__ReturnsOkIncludingReturnObjectWithError()
        {
            var result = _controller.GroupCodeDiscount("Incorrect");
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GroupCodeDiscount__NullString__ReturnsOkIncludingReturnObjectWithError()
        {
            var result = _controller.GroupCodeDiscount(null);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<CarDiscountPolicy>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }
        #endregion

        #region PaymentFrequencyDiscount tests
        [TestMethod]
        public void PaymentFrequencyDiscount__AnyValidInput__ReturnsOkIncludingReturnObjectDiscountModel()
        {
            var result = _controller.PaymentFrequencyDiscount(CorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<PaymentFrequencyDiscountModel>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.AreEqual(99m, response.Object.Amount);
        }

        [TestMethod]
        public void PaymentFrequencyDiscount__UnknownPaymentFrequency__ReturnsOkIncludingReturnObjectWithError()
        {
            var unknownPaymentModel = new CarViewModel() { Payment = new CarPayment() { PaymentFrequency = PaymentFrequency.Unknown} };
            var result = _controller.PaymentFrequencyDiscount(unknownPaymentModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<PaymentFrequencyDiscountModel>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void PaymentFrequencyDiscount__NullViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            CarViewModel nullModel = null;
            var result = _controller.PaymentFrequencyDiscount(nullModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<PaymentFrequencyDiscountModel>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        #endregion

        #region GetVariants tests
        [TestMethod]
        public void GetVariants__CorrectCarViewModel__ReturnsOkIncludingReturnObjectWithVariants()
        {
            var result = _controller.GetVariants(CorrectViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<ProductVariant>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(response.Object.Count > 0);
        }

        [TestMethod]
        public void GetVariants__IncorrectCarViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var incorrectModel = new CarViewModel() { };
            var result = _controller.GetVariants(incorrectModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<ProductVariant>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetVariants__DriverLessThan18__ReturnsOkIncludingReturnObjectWithError()
        {
            var result = _controller.GetVariants(YoungDriverCarViewModel);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<ProductVariant>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.IsTrue((response.ErrorMessages.FirstOrDefault().StartsWith("Acceptance check failed: ")));
        }

        #endregion

        #region GetCoverages
        [TestMethod]
        public void GetCoverages__CorrectCarViewModel__ReturnsOkIncludingReturnObjectCoverages()
        {
            var okResult = _controller.GetCoverages(CorrectViewModel) as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<Coverage>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsTrue(response.Object.Count > 0);
        }

        [TestMethod]
        public void GetCoverages__DriverLessThan18__ReturnsOkIncludingReturnObjectError()
        {
            var okResult = _controller.GetCoverages(YoungDriverCarViewModel) as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<Coverage>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.IsTrue((response.ErrorMessages.FirstOrDefault().StartsWith("Acceptance check failed: ")));
        }

        [TestMethod]
        public void GetCoverages__NullViewModel__ReturnsOkIncludingReturnObjectWithError()
        {
            var okResult = _controller.GetCoverages(null) as OkObjectResult;
            var response = okResult.Value as ReturnObject<List<Coverage>>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        #endregion

        #region GetVariantsAndCoverages
        [TestMethod]
        public void GetVariantsAndCoverages__CorrectCarViewModel__ReturnsOkIncludingReturnObjectCoveragesAndVariants()
        {
            var okResult = _controller.GetVariantsAndCoverages(CorrectViewModel) as OkObjectResult;
            var response = okResult.Value as ReturnObject<VariantsAndCoverages>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Object.Coverages);
            Assert.IsNotNull(response.Object.Variants);
        }

        [TestMethod]
        public void GetVariantsAndCoverages__NullViewModel__ReturnsOkIncludingReturnObjectError()
        {
            var okResult = _controller.GetVariantsAndCoverages(null) as OkObjectResult;
            var response = okResult.Value as ReturnObject<VariantsAndCoverages>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void GetVariantsAndCoverages__DriverLessThan18__ReturnsOkIncludingReturnObjectError()
        {
            var okResult = _controller.GetVariantsAndCoverages(YoungDriverCarViewModel) as OkObjectResult;
            var response = okResult.Value as ReturnObject<VariantsAndCoverages>;

            Assert.IsNotNull(okResult);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.IsTrue((response.ErrorMessages.FirstOrDefault().StartsWith("Acceptance check failed: ")));
        }
        
        #endregion
    }
}
