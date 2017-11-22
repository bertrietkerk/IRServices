using InsuranceRight.Services.Client.Models;
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

            //var model = new AddressCheckViewModel();
            return View();//model);
        }
        string BaseUrl = @"http://localhost:53491/api/addresscheck/";


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddressCheck(AddressCheckViewModel viewModel)
        {
            var zipCode = viewModel.ZipCode;
            var stringContent = new StringContent(JsonConvert.SerializeObject(zipCode), Encoding.UTF8, "application/json");
            var returnObject = new ReturnObject<ZipCode>();

            // call api with zipcode
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync("validatezipcode", stringContent);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    returnObject = JsonConvert.DeserializeObject<ReturnObject<ZipCode>>(response);
                    viewModel.ReturnZipCodeObject = returnObject;
                }
            }

            
            // check isValid value of the ReturnObject
            
            // set Validation message to message of the ReturnObject

            return View(viewModel);
        }
    }
}