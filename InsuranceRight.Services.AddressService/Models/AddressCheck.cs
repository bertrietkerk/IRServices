using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace InsuranceRight.Services.AddressService.Models
{
    public class AddressCheck : IAddressCheck
    {
        private static readonly string _defaultDutchRegexPattern = "^[1-9][0-9]{3}[A-Z]{2}$";
        private readonly IEnumerable<String> _validZipCodeList;
        private readonly IEnumerable<Address> _validAddressList;

        public AddressCheck(IDataProvider dataProvider)
        {
            _validAddressList = dataProvider.GetValidAddresses();
            _validZipCodeList = dataProvider.GetValidZipCodes(_validAddressList);
        }

        // TODO: do we want to be able to provide a different regex-pattern?
        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match default 'Dutch Zipcode' regex pattern </returns>
        public bool IsZipCodeValid(string zipCode)
        {
            // check if zipCode is valid based on the default dutch zipcode pattern
            return IsZipCodeValid(zipCode, _defaultDutchRegexPattern);
        }

        // TODO: do we want to be able to provide a different regex-pattern?
        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <param name="pattern">An optional custom regex pattern which the zipcode must conform to in order to be checked</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match regex pattern </returns>
        public bool IsZipCodeValid(string zipCode, string pattern)
        {
            if (string.IsNullOrEmpty(zipCode))
                throw new NullReferenceException("Zipcode cannot be null or empty string");
                // return false + statuscode 0001 "ValidateZipcode error: Zipcode cannot be null or empty string"

            // first check zipcode on regex, then check in list
            zipCode = zipCode.Replace(" ", "").ToUpper();
            Regex regex = new Regex(pattern);


            // TODO: Error handling on IR side by returning statuscodes
            if (!regex.IsMatch(zipCode))
                throw new NullReferenceException("Zipcode not correct format");
                // return false + statuscode 0002 "ValidateZipcode error: Zipcode is NOT in the CORRECT FORMAT"

            if (_validZipCodeList.Contains(zipCode))
                return true;
                // return true + custom statuscode 0000 success

            throw new NullReferenceException("Zipcode was not found to be valid");
            // return false + custom statuscode 0003 "ValidateZipcode error: Zipcode is right format, but NOT found in ValidZipcodeList"
        }

       


        /// <summary>
        /// Get the full address based on the zipcode, housenumber and housenumberextension
        /// </summary>
        /// <param name="zipCode">The zipcode of the full address</param>
        /// <param name="houseNumber">The housenumber of the full address</param>
        /// <param name="houseNumberExt">The housenumberextension of the full address</param>
        /// <returns>The full address based on zipcode, housenumber & housenumberextension, or null if not found in _validAddressList</returns>
        public Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension = "")
        {
            if (string.IsNullOrEmpty(zipCode) || string.IsNullOrEmpty(houseNumber))
                throw new NullReferenceException("Zipcode and housenumber cannot be null");
            // TODO: different exception/return value
            // return custom statuscode 0010 "GetFullAddress error: Zipcode cannot be null"
            // return custom statuscode 0011 "GetFullAddress error: Housenumber cannot be null"
            // 
            // return custom statuscode 0012 "GetFullAddress error: Combination of  cannot be null"


            zipCode = zipCode.Replace(" ", "").ToUpper();
            houseNumber = houseNumber.Trim();

            if (houseNumberExtension == null)
                houseNumberExtension = "";
            else if (!string.IsNullOrEmpty(houseNumberExtension))
                houseNumberExtension = houseNumberExtension.Trim().ToUpper();

            return _validAddressList.FirstOrDefault(a => a.ZipCode == zipCode && a.HouseNumber == houseNumber && a.HouseNumberExtension == houseNumberExtension);   
        }
    }
}
