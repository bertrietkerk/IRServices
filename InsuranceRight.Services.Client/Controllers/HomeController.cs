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
            return View();
        }




        string BaseUrl = @"http://localhost:53491/api/addresscheck/";

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddressCheck(AddressCheckViewModel viewModel)
        {
            if (viewModel.ReturnZipCodeObject == null || viewModel.ReturnZipCodeObject.IsValid == false)
            {
                ZipCode zipCodeModel = new ZipCode() { Zipcode = viewModel.Address.ZipCode };

                var stringContent = new StringContent(JsonConvert.SerializeObject(zipCodeModel), Encoding.UTF8, "application/json");
                var returnObjectZipcode = new ReturnObject<ZipCode>();

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
                        returnObjectZipcode = JsonConvert.DeserializeObject<ReturnObject<ZipCode>>(response);
                        viewModel.ReturnZipCodeObject = returnObjectZipcode;
                    }
                }
            }

            // zipcode is valid, now check full address
            var content = new StringContent(JsonConvert.SerializeObject(viewModel.Address), Encoding.UTF8, "application/json");
            var returnObjectAddress = new ReturnObject<Address>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage result = await client.PostAsync("getfulladdress", content);
                if (result.IsSuccessStatusCode)
                {
                    var response = result.Content.ReadAsStringAsync().Result;
                    returnObjectAddress = JsonConvert.DeserializeObject<ReturnObject<Address>>(response);
                    viewModel.ReturnAddressObject = returnObjectAddress;
                }
            }



            return View(viewModel);
        }
    }
}