using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Feature.Car.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/Car/Lookup")]
    public class LicensePlateLookupController : Controller
    {
        private readonly ILicensePlateLookup _lpLookup;

        public LicensePlateLookupController(ILicensePlateLookup lpLookup)
        {
            _lpLookup = lpLookup;
        }

        /// <summary>
        /// Returns the Car details of the car corresponding to the give license-plate
        /// </summary>
        /// <param name="licensePlate">The license-plate from the car to get the details from</param>
        /// <returns>CarObject with car details</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<CarObject>))]
        public IActionResult GetCarDetails([FromBody] string licensePlate)
        {
            var response = new ReturnObject<CarObject>();

            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                response.ErrorMessages.Add("Licenseplate was null or empty");
                return Ok(response);
            }

            var carDetails = _lpLookup.GetCar(licensePlate.ToString());
            if (carDetails == null)
            {
                response.ErrorMessages.Add("Car not found");
                return Ok(response);
            }

            response.Object = carDetails;
            return Ok(response);
        }
    }
}