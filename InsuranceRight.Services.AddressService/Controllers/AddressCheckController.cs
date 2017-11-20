using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.AddressService.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceRight.Services.AddressService.Controllers
{
    [Route("api/[controller]")]
    public class AddressCheckController : Controller
    {
        private static readonly string _defaultDutchRegexPattern = "^[1-9][0-9]{3}[A-Z]{2}$";
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

        // POST api/addresscheck/validatezipcode
        [HttpPost("[action]")]
        public IActionResult ValidateZipcode([FromBody]ZipCodeDto ZipCode)
        {
            string zipCodeString = ZipCode.ToString();
            if (string.IsNullOrEmpty(zipCodeString))
            {
                ZipCode.Status = ZipCodeStatus.EmptyOrNull;
                return new JsonResult(ZipCode);
                //return NotFound("Zipcode was null or empty string");
            }

            zipCodeString = zipCodeString.Replace(" ", "").ToUpper();
            Regex regex = new Regex(_defaultDutchRegexPattern);

            if (!regex.IsMatch(zipCodeString))
            {
                ZipCode.Status = ZipCodeStatus.IncorrectFormat;
                return new JsonResult(ZipCode);
                //return NotFound("Zipcode was not in a valid format");
            }

            var result = _addressCheckProvider.IsZipCodeValid(zipCodeString);

            if (result)
            {
                ZipCode.Status = ZipCodeStatus.Valid;
                return new JsonResult(ZipCode);
                //return Ok(ZipCode);
            }

            ZipCode.Status = ZipCodeStatus.NotFound;
            return new JsonResult(ZipCode);
            //return NotFound("Zipcode was not a valid zipcode");
        }

        // POST api/addresscheck/getfulladdress
        [HttpPost("[action]")]
        public IActionResult GetFullAddress([FromBody]AddressDto address)
        {
            if (string.IsNullOrWhiteSpace(address.ZipCode) || string.IsNullOrWhiteSpace(address.HouseNumber))
            {
                address.Status = AddressStatus.EmptyOrNull;
                return Json(address);
            }

            address.ZipCode = address.ZipCode.Replace(" ", "").ToUpper();
            address.HouseNumber = address.HouseNumber.Trim();

            if (address.HouseNumberExtension == null)
                address.HouseNumberExtension = "";
            else if (address.HouseNumberExtension != string.Empty)
                address.HouseNumberExtension = address.HouseNumberExtension.Trim().ToUpper();

            var result = _addressCheckProvider.GetFullAddress(address.ZipCode, address.HouseNumber, address.HouseNumberExtension);
            if (result == null)
            {
                address.Status = AddressStatus.NotFound;
                return Json(address);
            }
            else
            {
                address.Street = result.Street;
                address.City = result.City;
                address.Country = result.Country;
                address.Status = AddressStatus.Valid;
                return Json(address);
            }
        }





        /////
        // Old Get Methods
        /////

        // GET api/addresscheck/validatezipcode/1111AA
        [HttpGet("[action]/{zipcode}")]
        public IActionResult ValidateZipCode(string zipcode)
        {
            if (string.IsNullOrEmpty(zipcode))
                return NotFound("Zipcode was null or empty string");

            zipcode = zipcode.Replace(" ", "").ToUpper();
            Regex regex = new Regex(_defaultDutchRegexPattern);

            if (!regex.IsMatch(zipcode))
                return NotFound("Zipcode was not in a valid format");

            var result = _addressCheckProvider.IsZipCodeValid(zipcode);

            if (result)
                return new JsonResult(new { result, zipcode });

            return NotFound("Zipcode was not a valid zipcode");
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
    }
}
