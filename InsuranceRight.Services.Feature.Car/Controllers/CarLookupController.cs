using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using Swashbuckle.AspNetCore.SwaggerGen;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/Car/Lookup")]
    public class CarLookupController : Controller
    {
        private readonly ICarLookup _carLookup;

        public CarLookupController(ICarLookup carLookup)
        {
            _carLookup = carLookup;
        }

        /// <summary>
        /// Get all distinct brands of cars
        /// </summary>
        /// <returns>List of car-brands (string)</returns>
        [HttpGet("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<string>>))]
        //[SwaggerResponse(400, Type = typeof(ReturnObject<List<string>>))]
        public IActionResult GetBrands()
        {
            var response = new ReturnObject<List<string>>();

            var brands = _carLookup.GetBrands();
            if (brands == null || brands.Count < 1)
            {
                response.ErrorMessages.Add("No brands were found");
                return Ok(response);
            }

            response.Object = brands;
            return Ok(response);
        }


        /// <summary>
        /// Get all possible models of the provided brand car
        /// </summary>
        /// <param name="brand">Brand of the car</param>
        /// <returns>A list of models based on the provided car brand</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<string>>))]
        //[SwaggerResponse(400, Type = typeof(ReturnObject<List<string>>))]
        public IActionResult GetModels([FromBody] string brand)
        {
            var response = new ReturnObject<List<string>>();

            if (string.IsNullOrWhiteSpace(brand))
            {
                response.ErrorMessages.Add("Brand was null or empty string");
                return Ok(response);
            }

            var models = _carLookup.GetModels(brand);
            if (models == null)
            {
                response.ErrorMessages.Add("No models were found for the given brand name '" + brand + "'");
                return Ok(response);
            }

            response.Object = models;
            return Ok(response);
        }

        /// <summary>
        /// Get all editions of the provided brand and model car
        /// </summary>
        /// <param name="viewModel">Model containing a brand(string) and model(string)</param>
        /// <returns>A list of editions based on the provided car brand and model</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<string>>))]
        //[SwaggerResponse(400, Type = typeof(ReturnObject<List<string>>))]
        public IActionResult GetEditions([FromBody] CarLookupViewModel viewModel)
        {
            var response = new ReturnObject<List<string>>();

            if (string.IsNullOrWhiteSpace(viewModel.Brand) || string.IsNullOrWhiteSpace(viewModel.Model))
            {
                response.ErrorMessages.Add("Brand and/or Model was null or empty string");
                return Ok(response);
            }

            var editions = _carLookup.GetEditions(viewModel.Brand, viewModel.Model);
            if (editions == null)
            {
                response.ErrorMessages.Add("No editions were found for the combination of this brand and model");
                return Ok(response);
            }

            response.Object = editions;
            return Ok(response);
        }

        /// <summary>
        /// Get the details (weight and catalog value) of the provided car
        /// </summary>
        /// <param name="viewModel">Model containing a brand(string), model(string) and edition(string) of the car to get the weight for</param>
        /// <returns>The details of the provided car</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<Dictionary<string, decimal>>))]
        //[SwaggerResponse(400, Type = typeof(ReturnObject<Dictionary<string, decimal>>))]
        public IActionResult GetEditionDetails([FromBody] CarLookupViewModel viewModel)
        {
            var response = new ReturnObject<Dictionary<string, decimal>>();
            var dict = new Dictionary<string, decimal>();

            if (string.IsNullOrWhiteSpace(viewModel.Brand) || string.IsNullOrWhiteSpace(viewModel.Model) || string.IsNullOrWhiteSpace(viewModel.Edition))
            {
                response.ErrorMessages.Add("Brand, Model and/or Edition was null or empty string");
                return Ok(response);
            }

            var weight = _carLookup.GetWeight(viewModel.Brand, viewModel.Model, viewModel.Edition);
            var catalogValue = _carLookup.GetCatalogValue(viewModel.Brand, viewModel.Model, viewModel.Edition);
            if (weight == 0 || catalogValue == 0)
            {
                response.ErrorMessages.Add("No car was found for the combination of this brand, model and edition");
                return Ok(response);
            }

            dict.Add("weight", weight);
            dict.Add("catalogValue", catalogValue);
            response.Object = dict;
            return Ok(response);
        }


        /// <summary>
        /// Get the weight of the provided car 
        /// </summary>
        /// <param name="viewModel">Model containing a brand(string), model(string) and edition(string) of the car to get the weight for</param>
        /// <returns>The weight(decimal) of the provided car</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<decimal>))]
        //[SwaggerResponse(400, Type = typeof(ReturnObject<decimal>))]
        public IActionResult GetWeight([FromBody] CarLookupViewModel viewModel)
        {
            var response = new ReturnObject<decimal>();

            if (string.IsNullOrWhiteSpace(viewModel.Brand) || string.IsNullOrWhiteSpace(viewModel.Model) || string.IsNullOrWhiteSpace(viewModel.Edition))
            {
                response.ErrorMessages.Add("Brand, Model and/or Edition was null or empty string");
                return Ok(response);
            }

            var weight = _carLookup.GetWeight(viewModel.Brand, viewModel.Model, viewModel.Edition);
            if (weight == 0)
            {
                response.ErrorMessages.Add("No car was found for the combination of this brand, model and edition");
                return Ok(response);
            }

            response.Object = weight;
            return Ok(response);
        }


        /// <summary>
        /// Get the catalog value of the provided car 
        /// </summary>
        /// <param name="viewModel">Model containing a brand(string), model(string) and edition(string) of the car to get the catalog value for</param>
        /// <returns>The catalog value(decimal) of the provided car</returns>
        [HttpPost("[action]")]
        [SwaggerResponse(200, Type = typeof(ReturnObject<decimal>))]
        public IActionResult GetCatalogValue([FromBody] CarLookupViewModel viewModel)
        {
            var response = new ReturnObject<decimal>();
            if (string.IsNullOrWhiteSpace(viewModel.Brand) || string.IsNullOrWhiteSpace(viewModel.Model) || string.IsNullOrWhiteSpace(viewModel.Edition))
            {
                response.ErrorMessages.Add("Brand, Model and/or Edition was null or empty string");
                return Ok(response);
            }

            var catalogValue = _carLookup.GetCatalogValue(viewModel.Brand, viewModel.Model, viewModel.Edition);
            if (catalogValue == 0)
            {
                response.ErrorMessages.Add("No car was found for the combination of this brand, model and edition");
                return Ok(response);
            }
            response.Object = catalogValue;
            return Ok(response);
        }
    }
}