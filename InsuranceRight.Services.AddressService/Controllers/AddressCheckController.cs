using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuranceRight.Services.AddressService.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using InsuranceRight.Services.Shared;

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

        // POST api/addresscheck/validatezipcode
        [HttpPost("[action]")]
        public IActionResult ValidateZipcode([FromBody]ZipCode ZipCode)
        {
            var response = new ReturnObject<ZipCode>() { IsValid = false, Message = "", Object = ZipCode };

            // TODO: get only string from body directly as zipcode
            string zipcode = ZipCode.ToString();
            if (string.IsNullOrWhiteSpace(zipcode))
            {
                response.Message = "Zipcode was null or empty";
                return Ok(response);
            }

            zipcode = zipcode.Replace(" ", "").ToUpper();
            Regex regex = new Regex(_defaultDutchRegexPattern);

            if (!regex.IsMatch(zipcode))
            {
                response.Message = "Zipcode was incorrect format (e.g. 1111AA)";
                return Ok(response);
            }

            var result = _addressCheckProvider.IsZipCodeValid(zipcode);

            if (result)
            {
                response.IsValid = true;
                response.Message = "Zipcode was found!";
                return Ok(response);
            }

            response.Message = "Zipcode was not found";
            return Ok(response);
        }

        // POST api/addresscheck/getfulladdress
        [HttpPost("[action]")]
        public IActionResult GetFullAddress([FromBody]Address address)
        {
            var response = new ReturnObject<Address>() { IsValid = false, Message = "", Object = address };

            if (address == null)
            {
                response.Message = "Address was null";
                return Ok(response);
            }

            if (string.IsNullOrWhiteSpace(address.ZipCode) || string.IsNullOrWhiteSpace(address.HouseNumber))
            {
                response.Message = "'Zipcode' and/or 'housenumber' of address was/were null or empty";
                return Ok(response);
            }

            address.ZipCode = address.ZipCode.Replace(" ", "").ToUpper();
            address.HouseNumber = address.HouseNumber.Trim();

            if (address.HouseNumberExtension == null)
                address.HouseNumberExtension = string.Empty;
            else if (address.HouseNumberExtension != string.Empty)
                address.HouseNumberExtension = address.HouseNumberExtension.Trim().ToUpper();

            Address result = _addressCheckProvider.GetFullAddress(address.ZipCode, address.HouseNumber, address.HouseNumberExtension);
            if (result != null)
            {
                response.IsValid = true;
                response.Message = "Address found!";
                response.Object = result;
                return Ok(response);
            }
            response.Message = $"No combination was found for {address.ZipCode} {address.HouseNumber} {address.HouseNumberExtension}";
            return Ok(response);
        }
    }
}
