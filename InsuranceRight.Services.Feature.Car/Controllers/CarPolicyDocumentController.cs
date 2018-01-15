﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Models.Car.ViewModels;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/CarPolicyDocument")]
    public class CarPolicyDocumentController : Controller
    {
        private readonly ICarDocumentService _documentService;

        public CarPolicyDocumentController(ICarDocumentService documentService)
        {
            _documentService = documentService;
        }


        /// <summary>
        /// Get the policy documents for the chosen insurance
        /// </summary>
        /// <param name="viewModel">CarViewModel</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult GetDocuments([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<IEnumerable<PolicyDocument>>();
            var documents = _documentService.GetDocuments(viewModel);
            if (documents == null)
            {
                response.ErrorMessages.Add("Provided viewmodel was null");
            }

            response.Object = documents;
            return Ok(response);
        }
    }
}