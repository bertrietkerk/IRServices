﻿using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Feature.Car.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/Car/Premium")]
    public class CarDiscountPolicyController : Controller
    {
        private readonly ICarDiscountPolicy _carDiscountPolicy;

        public CarDiscountPolicyController(ICarDiscountPolicy discountPolicy)
        {
            _carDiscountPolicy = discountPolicy;
        }

        /// <summary>
        /// Gets the discount amount(decimal) based on the provided group code
        /// </summary>
        /// <param name="discountCode">Discount code</param>
        /// <returns>Amount(decimal) of discount</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<CarDiscountPolicy>))]
        public IActionResult GroupDiscounts([FromBody] string discountCode)
        {
            ReturnObject<CarDiscountPolicy> response = new ReturnObject<CarDiscountPolicy>();
            response.Object = _carDiscountPolicy.GetDiscountForGroup(discountCode);
            
            return Ok(_carDiscountPolicy.GetDiscountForGroup(discountCode));
        }

    }
}