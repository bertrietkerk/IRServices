using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Response;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]")]
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
        public IActionResult GetCarDetails([FromBody] LicensePlate licensePlate)
        {
            var response = new ReturnObject<CarObject>();
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