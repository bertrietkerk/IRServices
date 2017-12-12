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

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarPremiumPolicyController : Controller
    {
        private readonly ICarPremiumPolicy _carPremiumPolicy;

        public CarPremiumPolicyController(ICarPremiumPolicy carPremiumPolicy)
        {
            _carPremiumPolicy = carPremiumPolicy;
        }

        /// <summary>
        /// Get Coverage Variants
        /// </summary>
        /// <param name="viewModel">Viewmodel containing PremiumFactors</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetVariants([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<ProductVariant>>();


            if (viewModel == null)
            {
                response.ErrorMessages.Add("Viewmodel was null");
                return Ok(response);
            }

            var variants = _carPremiumPolicy.GetVariants(
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
    }
}