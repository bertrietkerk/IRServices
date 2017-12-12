using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.AddressService.Interfaces;
using System.Collections.Generic;
using InsuranceRight.Services.Models.Foundation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuranceRight.Services.AddressService.Controllers
{
    /// <summary>
    /// Controller for validating zipcodes and getting full address details
    /// </summary>
    [Route("api/[controller]")]
    public class AddressLookupController : Controller
    {
        private static readonly string _defaultDutchRegexPattern = "^[1-9][0-9]{3}[A-Z]{2}$";
        IAddressLookup _addressCheckProvider;

        /// <summary>
        /// Constructor injecting IAddressCheck
        /// </summary>
        /// <param name="addressCheck">IAddressCheck</param>
        public AddressLookupController(IAddressLookup addressCheck)
        {
            _addressCheckProvider = addressCheck;
        }

        // POST api/addresscheck/validatezipcode
        /// <summary>
        /// Method for validating a zipcode
        /// </summary>
        /// <param name="ZipCode">The zipcode to validate</param>
        /// <returns>ReturnObject including ErrorMessage(s) if the request was invalid</returns>
        [HttpPost("[action]")]
        public IActionResult ValidateZipcode([FromBody]ZipCode ZipCode)
        {
            var response = new ReturnObject<ZipCode>() { ErrorMessages = new List<string>(), Object = ZipCode };
            if (ZipCode == null)
            {
                response.ErrorMessages.Add("ZipCode was null");
                return Ok(response);
            }

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

            if (_addressCheckProvider.IsZipCodeValid(zipcode))
            {
                response.ErrorMessages.Clear();
                return Ok(response);
            }

            response.ErrorMessages.Add("Zipcode was not found");
            return Ok(response);
        }

        // POST api/addresscheck/getfulladdress
        /// <summary>
        /// Method for getting the full details of an address
        /// </summary>
        /// <param name="address">Incomplete Address to fill with details</param>
        /// <returns>ReturnObject including ErrorMessage(s) if request was invalid, and an object which will include all details if request is valid</returns>
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
