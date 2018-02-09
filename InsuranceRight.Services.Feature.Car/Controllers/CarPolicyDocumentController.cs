using System.Collections.Generic;
using System.Linq;
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
        [SwaggerResponse(200, Type = typeof(ReturnObject<List<PolicyDocument>>))]
        public IActionResult Documents([FromBody] CarViewModel viewModel)
        {
            var response = new ReturnObject<List<PolicyDocument>>();

            if(HelperMethods.Helpers.IsAnyObjectNull(new object[] { viewModel, viewModel?.Payment, viewModel?.PremiumFactors, viewModel?.PremiumFactors?.Car, viewModel?.PremiumFactors?.Driver, viewModel?.PremiumFactors?.Driver?.ResidenceAddress, viewModel?.PremiumFactors?.Car?.Price }))
            {
                response.ErrorMessages.Add("Viewmodel wasn't complete");
                return Ok(response);
            }

            var documents = _documentService.GetDocuments(viewModel);
            if (documents == null)
            {
                response.ErrorMessages.Add("Something went wrong generating the documents");
            }

            response.Object = documents;
            return Ok(response);
        }
    }
}