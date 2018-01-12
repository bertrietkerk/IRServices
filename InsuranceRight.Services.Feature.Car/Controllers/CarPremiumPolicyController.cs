using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Car.ViewModels;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Coverages;
using Microsoft.Extensions.Options;
using InsuranceRight.Services.Models.Settings;
using InsuranceRight.Services.Acceptance.Services;
using InsuranceRight.Services.Models.Acceptance;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarPremiumPolicyController : Controller
    {
        private readonly ICarPremiumPolicy _carPremiumPolicy;
        private readonly PremiumCalculationSettings _settings;
        private readonly IAcceptanceCheck _acceptanceCheck;

        public CarPremiumPolicyController(ICarPremiumPolicy carPremiumPolicy, IOptions<PremiumCalculationSettings> settings, IAcceptanceCheck acceptanceCheck)
        {
            _carPremiumPolicy = carPremiumPolicy;
            _settings = settings.Value;
            _acceptanceCheck = acceptanceCheck;
        }

        /// <summary>
        /// Get Product Variants
        /// </summary>
        /// <param name="viewModel">Viewmodel containg licenseplate, driver-age, -damagefreeyears and -residenceaddress zipcode</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult GetVariants_Old([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<ProductVariant>>();
            if (viewModel == null || viewModel.PremiumFactors.Car == null || viewModel.PremiumFactors.Driver == null || viewModel.PremiumFactors.Driver.ResidenceAddress == null)
            {
                response.ErrorMessages.Add("Viewmodel was null");
                return Ok(response);
            }

            var variants = _carPremiumPolicy.GetVariants_Old(
                viewModel.PremiumFactors.Car.LicensePlate,
                viewModel.PremiumFactors.Driver.Age,
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
        public IActionResult GetVariants([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<ProductVariant>>();

            if (viewModel == null || viewModel.PremiumFactors.Car == null || viewModel.PremiumFactors.Driver == null || viewModel.PremiumFactors.Driver.ResidenceAddress == null)
            {
                response.ErrorMessages.Add("Viewmodel was null");
                return Ok(response);
            }

            var variants = _carPremiumPolicy.GetVariants(
                viewModel.PremiumFactors.Car.LicensePlate,
                viewModel.PremiumFactors.Driver.Age,
                viewModel.PremiumFactors.Driver.DamageFreeYears,
                viewModel.PremiumFactors.Driver.ResidenceAddress.ZipCode,
                viewModel.PremiumFactors.Driver.KilometersPerYear
            );

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
        public IActionResult GetCoverages([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<Coverage>>();

            if (viewModel == null || viewModel.PremiumFactors.Car == null || viewModel.PremiumFactors.Driver == null || viewModel.PremiumFactors.Driver.ResidenceAddress == null)
            {
                response.ErrorMessages.Add("Viewmodel was null");
                return Ok(response);
            }

            var coverages = _carPremiumPolicy.GetCoverages(
                viewModel.PremiumFactors.Car.LicensePlate,
                viewModel.PremiumFactors.Driver.Age,
                viewModel.PremiumFactors.Driver.DamageFreeYears,
                viewModel.PremiumFactors.Driver.ResidenceAddress.ZipCode);

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
        public IActionResult GetVariantsAndCoverages([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<VariantsAndCoverages>();
            var car = viewModel.PremiumFactors.Car;
            var driver = viewModel.PremiumFactors.Driver;

            if (viewModel == null || car == null || driver == null || driver.ResidenceAddress == null)
            {
                response.ErrorMessages.Add("Viewmodel was null");
                return Ok(response);
            }

            // ACCEPTANCE CHECK
            if (_settings.IncludeAcceptanceCheck)
            {
                AcceptanceStatus status = _acceptanceCheck.Check(driver, car);
                if (!status.IsAccepted)
                {
                    response.ErrorMessages.Add("Acceptance check failed: " + status.Reason);
                    return Ok(response);
                }
            }

            response.Object = _carPremiumPolicy.GetVariantsAndCoverages(car.LicensePlate, driver.Age, driver.DamageFreeYears, driver.ZipCode, driver.KilometersPerYear);

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
        public IActionResult PaymentFrequencyDiscount([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<PaymentFrequencyDiscountModel>();
            response.Object = new PaymentFrequencyDiscountModel();

            if (viewModel == null || viewModel.Payment == null)
            {
                response.ErrorMessages.Add("Viewmodel was null");
                response.Object.Amount = 0M;
                return Ok(response);
            }

            var discount = _carPremiumPolicy.GetPaymentFrequencyDiscount((int)viewModel.Payment.PaymentFrequency);

            response.Object.Frequency = viewModel.Payment.PaymentFrequency;
            response.Object.Amount = discount;
            return Ok(response);
        }
    }
}