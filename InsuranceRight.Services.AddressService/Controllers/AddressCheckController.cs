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
            return new string[] {
                "Add '/zipcode' behind the url to validate the zipcode",
                "Add '/zipcode/housenumber/?housenumberextension' behind the url to check if address is valid"
            };
        }


        //////////////////////////////////////////////////////////
        // TODO:    url/param1/param2   or   url?param1=..&param2=..
        //////////////////////////////////////////////////////////

        // GET api/addresscheck/validatezipcode/1111AA
        [HttpGet("[action]/{zipcode}")]
        public bool ValidateZipCode(string zipcode)
        {
            return _addressCheckProvider.IsZipCodeValid(zipcode);
        }

        // GET api/addresscheck/validatezipcode?zipcode=1111AA
        [HttpGet("[action]")]
        public bool TestValidateZipCode([FromQuery]string zipcode)
        {
            return _addressCheckProvider.IsZipCodeValid(zipcode);
        }

        // GET api/addresscheck/getfulladdress/1111AA/1
        // GET api/addresscheck/getfulladdress/1111AA/1/a
        [HttpGet("[action]/{zipcode}/{housenumber}/{housenumberextension?}")]
        public IActionResult GetFullAddress(string zipcode, string housenumber, string housenumberextension = "")
        {
            var address = _addressCheckProvider.GetFullAddress(zipcode, housenumber, housenumberextension);
            if (address == null)
                return NotFound();

            return new JsonResult(address);
        }

        // GET api/addresscheck/getfulladdress?zipcode=1111AA&housenumber=1
        // GET api/addresscheck/getfulladdress?zipcode=1111AA&housenumber=1&housenumberextension=a
        [HttpGet("[action]")]
        public IActionResult TestGetFullAddress([FromQuery]string zipcode, string housenumber, string housenumberextension = "")
        {
            var address = _addressCheckProvider.GetFullAddress(zipcode, housenumber, housenumberextension);
            if (address == null)
                return NotFound();

            return new JsonResult(address);
        }
    }
}
