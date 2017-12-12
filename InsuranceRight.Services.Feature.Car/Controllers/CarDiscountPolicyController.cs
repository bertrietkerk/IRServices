using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Car;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarDiscountPolicyController : Controller
    {
        private readonly ICarDiscountPolicy _carDiscountPolicy;

        public CarDiscountPolicyController(ICarDiscountPolicy discountPolicy)
        {
            _carDiscountPolicy = discountPolicy;
        }

        /// <summary>
        /// Gets the discount amount(decimal) based on the provided code
        /// </summary>
        /// <param name="discountCode">Discount code</param>
        /// <returns>Amount(decimal) of discount</returns>
        [HttpPost]
        public IActionResult GroupDiscounts([FromBody] string discountCode)
        {
            return Ok(_carDiscountPolicy.GetDiscountForGroup(discountCode));
        }

    }
}