using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.Feature.Car.Services;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InsuranceRight.Services.Feature.Car.Controllers
{
    [Produces("application/json")]
    [Route("api/Car/Policy")]
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
        [SwaggerResponse(200, Type = typeof(ReturnObject<IEnumerable<PolicyDocument>>))]
        public IActionResult Documents([FromBody] CarViewModel viewModel)
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