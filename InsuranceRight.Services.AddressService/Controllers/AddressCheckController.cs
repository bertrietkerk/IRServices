using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.AddressService.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceRight.Services.AddressService.Controllers
{
    [Route("api/[controller]")]
    public class AddressCheckController : Controller
    {
        IAddressCheck _addressCheckProvider;

        public AddressCheckController(IAddressCheck addressCheck)
        {
            this._addressCheckProvider = addressCheck;
        }


        [HttpGet]
        public string[] Get()
        {
            return new string[] { "Add '/zipcode' behind the url to validate the zipcode", "Add '/zipcode/housenumber/?housenumberextension' behind the url to check if address is valid" };
        }

        // GET api/addresscheck/1111AA
        [HttpGet("{zipcode}")]
        public bool ValidateZipCode(string zipcode)
        {
            return _addressCheckProvider.ValidateZipCode(zipcode);
        }

        // GET api/addresscheck/2222BB/2
        [HttpGet("{zipcode}/{housenumber}")]
        public IActionResult GetFullAddress(string zipcode, string housenumber)
        {
            var address = _addressCheckProvider.GetFullAddress(zipcode, housenumber);
            if (address == null)
            {
                address = new Address()
                {
                    ZipCode = zipcode,
                    HouseNumber = housenumber
                };
                // return as JSON
                var returnJson = JsonConvert.SerializeObject(address, Formatting.Indented);
                //string[] returnObject = new string[] { zipcode, housenumber };
                var request = Request;
                return NotFound(request);
            }

            return new JsonResult(address);
        }

        // GET api/addresscheck/2222BB/99/b
        [HttpGet("{zipcode}/{housenumber}/{extension}")]
        public IActionResult GetFullAddress(string zipcode, string housenumber, string extension)
        {
            var address = _addressCheckProvider.GetFullAddress(zipcode, housenumber, extension);
            if (address == null)
            {
                address = new Address()
                {
                    ZipCode = zipcode,
                    HouseNumber = housenumber,
                    HouseNumberExtension = extension
                }; 
                // return as JSON
                var returnJson = JsonConvert.SerializeObject(address, Formatting.Indented);
                return NotFound(returnJson);
            }

            return new JsonResult(address);
        }

    }
}
