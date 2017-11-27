using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using InsuranceRight.Services.Shared.Models;
using InsuranceRight.Services.AddressService.Interfaces;
using System.Collections.Generic;

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
            var response = new ReturnObject<ZipCode>() { ErrorMessages = new List<string>(), Object = ZipCode };

            // TODO: get only string from body directly as zipcode
            string zipcode = ZipCode.ToString();
            if (string.IsNullOrWhiteSpace(zipcode))
            {
                response.ErrorMessages.Add("Zipcode was null or empty");
                return Ok(response);
            }

            zipcode = zipcode.Replace(" ", "").ToUpper();
            Regex regex = new Regex(_defaultDutchRegexPattern);

            if (!regex.IsMatch(zipcode))
            {
                response.ErrorMessages.Add("Zipcode wasn't in the correct format (e.g. 1111AA)");
                return Ok(response);
            }

            var result = _addressCheckProvider.IsZipCodeValid(zipcode);

            if (result)
            {
                response.ErrorMessages.Clear();
                return Ok(response);
            }

            response.ErrorMessages.Add("Zipcode was not found");
            return Ok(response);
        }

        // POST api/addresscheck/getfulladdress
        [HttpPost("[action]")]
        public IActionResult GetFullAddress([FromBody]Address address)
        {
            var response = new ReturnObject<Address>() { ErrorMessages = new List<string>() , Object = address };

            if (address == null)
            {
                response.ErrorMessages.Add("Address was null");
                return Ok(response);
            }

            if (string.IsNullOrWhiteSpace(address.ZipCode) || string.IsNullOrWhiteSpace(address.HouseNumber))
            {
                response.ErrorMessages.Add("'Zipcode' and/or 'housenumber' of address cannot be null or empty");
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
                response.ErrorMessages.Clear();
                response.Object = result;
                return Ok(response);
            }

            response.ErrorMessages.Add($"No combination was found for {address.ZipCode} {address.HouseNumber}{address.HouseNumberExtension}");
            return Ok(response);
        }
    }
}
