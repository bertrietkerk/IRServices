using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using Microsoft.Extensions.Options;
using InsuranceRight.Services.Models.Settings;
using InsuranceRight.Services.Acceptance.Services;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Feature.Car.Models.Response;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Models.Coverages;
using InsuranceRight.Services.Feature.Car.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/Car/Premium")]
    public class CarPremiumPolicyController : Controller
    {
        private readonly ICarPremiumPolicy _carPremiumPolicy;
        private readonly ICarDiscountPolicy _carDiscountPolicy;
        private readonly PremiumCalculationSettings _settings;
        private readonly ICarAcceptance _acceptance;

        public CarPremiumPolicyController(ICarPremiumPolicy carPremiumPolicy, ICarDiscountPolicy carDiscountPolicy, IOptions<PremiumCalculationSettings> settings, ICarAcceptance acceptance)
        {
            _carPremiumPolicy = carPremiumPolicy;
            _carDiscountPolicy = carDiscountPolicy;
            _settings = settings.Value;
            _acceptance = acceptance;
        }

        /// <summary>
        /// Get Product Variants
        /// </summary>
        /// <param name="viewModel">Viewmodel containg licenseplate, driver-age, -damagefreeyears and -residenceaddress zipcode</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<ProductVariant>>))]
        [Obsolete("This action is not supported anymore. Please use 'GetVariants' action")]
        public IActionResult GetVariants_Old([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<ProductVariant>>();
            if (HelperMethods.Helpers.IsAnyObjectNull(new object[] { viewModel, viewModel?.PremiumFactors?.Car, viewModel?.PremiumFactors?.Driver, viewModel?.PremiumFactors?.Driver?.ResidenceAddress }))
            {
                response.ErrorMessages.Add("Viewmodel/premiumfactors/car/driver/residenceaddress was null");
                return Ok(response);
            }

            var variants = _carPremiumPolicy.GetVariants_Old(
                viewModel.PremiumFactors.Car.LicensePlate,
                viewModel.PremiumFactors.Driver.BirthDate,
                viewModel.PremiumFactors.Driver.DamageFreeYears,
                viewModel.PremiumFactors.Driver.ResidenceAddress.ZipCode);

            if (variants == null)
            {
                response.ErrorMessages.Add("Variants were not found");
                return Ok(response);
            }

            response.Object = variants;
            return Ok(response);
        }


        /// <summary>
        /// Get Package Variants (MTPL || MTPL Limited Casco || MTPL All Risk)
        /// </summary>
        /// <param name="viewModel">Viewmodel containg licenseplate, driver-age, -damagefreeyears, -residenceaddress zipcode and -kilometersPerYear</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<ProductVariant>>))]
        public IActionResult GetVariants([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<ProductVariant>>();

            if (HelperMethods.Helpers.IsAnyObjectNull(new object[] { viewModel, viewModel?.PremiumFactors?.Car, viewModel?.PremiumFactors?.Driver, viewModel?.PremiumFactors?.Driver?.ResidenceAddress }))
            {
                response.ErrorMessages.Add("Viewmodel was null");
                return Ok(response);
            }

            var car = viewModel.PremiumFactors.Car;
            var driver = viewModel.PremiumFactors.Driver;

            var variants = _carPremiumPolicy.GetVariants(car.LicensePlate, driver.BirthDate, driver.DamageFreeYears, driver.ResidenceAddress.ZipCode, driver.KilometersPerYear);
            if (variants == null)
            {
                response.ErrorMessages.Add("Variants were not found");
                return Ok(response);
            }

            response.Object = variants;
            return Ok(response);
        }



        /// <summary>
        /// Get Coverages
        /// </summary>
        /// <param name="viewModel">Viewmodel containg licenseplate, driver-age, -damagefreeyears and -residenceaddress zipcode</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<Coverage>>))]
        public IActionResult GetCoverages([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<Coverage>>();

            if (HelperMethods.Helpers.IsAnyObjectNull(new object[] { viewModel, viewModel?.PremiumFactors?.Car, viewModel?.PremiumFactors?.Driver, viewModel?.PremiumFactors?.Driver?.ResidenceAddress }))
            {
                response.ErrorMessages.Add("Viewmodel/premiumfactors/car/driver/residenceaddress was null");
                return Ok(response);
            }
            var car = viewModel.PremiumFactors.Car;
            var driver = viewModel.PremiumFactors.Driver;
            var coverages = _carPremiumPolicy.GetCoverages(car.LicensePlate, driver.BirthDate, driver.DamageFreeYears, driver.ResidenceAddress.ZipCode);

            if (coverages == null)
            {
                response.ErrorMessages.Add("Coverages were not found");
                return Ok(response);
            }

            response.Object = coverages;
            return Ok(response);
        }

        /// <summary>
        /// Get Coverages and Variants
        /// </summary>
        /// <param name="viewModel">Viewmodel containg licenseplate, driver-age, -damagefreeyears and -residenceaddress zipcode</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<VariantsAndCoverages>))]
        public IActionResult GetVariantsAndCoverages([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<VariantsAndCoverages>();

            if (HelperMethods.Helpers.IsAnyObjectNull(new object[] { viewModel, viewModel?.PremiumFactors?.Car, viewModel?.PremiumFactors?.Driver, viewModel?.PremiumFactors?.Driver?.ResidenceAddress }))
            {
                response.ErrorMessages.Add("Viewmodel/premiumfactors/car/driver/residenceaddress was null");
                return Ok(response);
            }

            var car = viewModel.PremiumFactors.Car;
            var driver = viewModel.PremiumFactors.Driver;

            if (_settings.IncludeAcceptanceCheck)
            {
                AcceptanceStatus status = _acceptance.Check(driver, car);
                if (!status.IsAccepted)
                {
                    response.ErrorMessages.Add("Acceptance check failed: " + status.Reason);
                    return Ok(response);
                }
            }
            response.Object = _carPremiumPolicy.GetVariantsAndCoverages(car.LicensePlate, driver.BirthDate, driver.DamageFreeYears, driver.ZipCode, driver.KilometersPerYear);

            if (response.Object == null)
                response.ErrorMessages.Add("Variants/coverages were not found");
            
            return Ok(response);
        }



        /// <summary>
        /// Get the discount based on the payment frequency
        /// </summary>
        /// <param name="viewModel">Viewmodel containing the payment frequency</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<PaymentFrequencyDiscountModel>))]
        public IActionResult PaymentFrequencyDiscount([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<PaymentFrequencyDiscountModel> { Object = new PaymentFrequencyDiscountModel() { Amount = 0M } };

            if (HelperMethods.Helpers.IsAnyObjectNull(new object[] { viewModel, viewModel?.Payment }))
            {
                response.ErrorMessages.Add("Viewmodel/payment was null");
                return Ok(response);
            }

            if (viewModel.Payment.PaymentFrequency == InsuranceRight.Services.Models.Enums.PaymentFrequency.Unknown)
            {
                response.ErrorMessages.Add("Payment frequency was Unknown");
                return Ok(response);
            }

            response.Object.Frequency = viewModel.Payment.PaymentFrequency;
            response.Object.Amount = _carPremiumPolicy.GetPaymentFrequencyDiscount((int)viewModel.Payment.PaymentFrequency);
            return Ok(response);
        }

        /// <summary>
        /// Gets the discount amount(decimal) based on the provided group code
        /// </summary>
        /// <param name="discountCode">Discount code</param>
        /// <returns>Amount(decimal) of discount</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<CarDiscountPolicy>))]
        public IActionResult GroupCodeDiscount([FromBody] string discountCode)
        {
            ReturnObject<CarDiscountPolicy> response = new ReturnObject<CarDiscountPolicy>();
            if (string.IsNullOrWhiteSpace(discountCode))
            {
                response.ErrorMessages.Add("Discount code was null or empty");
                return Ok(response);
            }

            response.Object = _carDiscountPolicy.GetDiscountForGroup(discountCode);

            if (!response.Object.IsDiscountFound)
                response.ErrorMessages.Add("Discount was not found");

            return Ok(response);
        }
    }
}