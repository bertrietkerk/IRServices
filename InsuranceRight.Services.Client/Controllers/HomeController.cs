using InsuranceRight.Services.Client.Models;
using InsuranceRight.Services.Client.Repositories;
using InsuranceRight.Services.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InsuranceRight.Services.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAddressApiCallsRepository _apiRepo;
        
        public HomeController() //IAddressApiCallsRepository ApiRepository)   <= DI configured in MVC .Net 461 app (Ninject?)
        {
            _apiRepo = new AddressApiCallsRepository();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddressCheck()
        {
            return View();
        }

        
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddressCheck(AddressCheckViewModel viewModel)
        {
            ViewBag.Street = "";
            ViewBag.City = "";
            ViewBag.Country = "";

            if (viewModel.ReturnZipCodeObject == null || viewModel.ReturnZipCodeObject.HasErrors == true)
            {
                ZipCode zipCode = new ZipCode() { Zipcode = viewModel.Address.ZipCode };
                viewModel.ReturnZipCodeObject = await _apiRepo.GetZipCodeReturnObjectAsync(zipCode);
            }

            // if zipcode is valid, check full address
            if (viewModel.ReturnZipCodeObject != null && viewModel.ReturnZipCodeObject.HasErrors == false)
            {
                viewModel.ReturnAddressObject = await _apiRepo.GetAddressReturnObjectAsync(viewModel.Address);
                if (!viewModel.ReturnAddressObject.HasErrors)
                {
                    ViewBag.Street = viewModel.ReturnAddressObject.Object.Street;
                    ViewBag.City = viewModel.ReturnAddressObject.Object.City;
                    ViewBag.Country = viewModel.ReturnAddressObject.Object.Country;
                }
            }
            
            return View(viewModel);
        }
    }
}