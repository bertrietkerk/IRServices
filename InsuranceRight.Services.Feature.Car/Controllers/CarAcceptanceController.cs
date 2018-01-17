using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/Acceptance")]
    public class AcceptanceController : Controller
    {
        private readonly ICarAcceptance _acceptance;
        private readonly IOptions<AcceptanceSettings> _settings;

        public AcceptanceController(ICarAcceptance acceptance, IOptions<AcceptanceSettings> settings)
        {
            _acceptance = acceptance;
            _settings = settings;
        }

        /// <summary>
        /// Acceptance check on current data
        /// </summary>
        /// <param name="viewModel">Current data. Can include -1- Car details (licenseplate, price, security measurements, etc), -2- Most frequent driver details, -3- Risk assesment answers </param>
        /// <returns>Acceptance status indicating accepted or not, and if not the reason</returns>
        [HttpPost("[action]")]
        public IActionResult Check([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<AcceptanceStatus>() { Object = new AcceptanceStatus() { IsAccepted = false } };
            var car = viewModel.PremiumFactors.Car;

            if (car == null || car.Price == null || car.Price.CatalogPrice == 0)
            {
                response.ErrorMessages.Add("Car or price was null");
                return Ok(response);
            }

            var result = _acceptance.Check(viewModel.PremiumFactors.Driver, car);
            if (!result.IsAccepted)
            {
                response.ErrorMessages.Add("Not accepted: see object for reason");
            }

            response.Object = result;
            return Ok(response);
        }
    }
}