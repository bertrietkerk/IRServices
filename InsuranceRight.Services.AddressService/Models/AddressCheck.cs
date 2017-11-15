﻿using System;
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


        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match default 'Dutch Zipcode' regex pattern </returns>
        public bool IsZipCodeValid(string zipCode)
        {
            return IsZipCodeValid(zipCode, _defaultDutchRegexPattern);
        }

        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <param name="pattern">An optional custom regex pattern which the zipcode must conform to in order to be checked</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match regex pattern </returns>
        public bool IsZipCodeValid(string zipCode, string pattern)
        {
            zipCode = zipCode.Replace(" ", "").ToUpper();
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(zipCode))
                return false;
            
            return _validZipCodeList.Contains(zipCode);
        }

        
        /// <summary>
        /// Get the full address based on the zipcode, housenumber, and empty string as housenumberextension
        /// </summary>
        /// <param name="zipCode">The zipcode of the full address</param>
        /// <param name="houseNumber">The housenumber of the full address</param>
        /// <returns>The full address based on zipcode & housenumber, or null if not found in _validAddressList</returns>
        public Address GetFullAddress(string zipCode, string houseNumber)
        {
            return GetFullAddress(zipCode, houseNumber, string.Empty);
        }


        /// <summary>
        /// Get the full address based on the zipcode, housenumber and housenumberextension
        /// </summary>
        /// <param name="zipCode">The zipcode of the full address</param>
        /// <param name="houseNumber">The housenumber of the full address</param>
        /// <param name="houseNumberExt">The housenumberextension of the full address</param>
        /// <returns>The full address based on zipcode, housenumber & housenumberextension, or null if not found in _validAddressList</returns>
        public Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension)
        {
            if (string.IsNullOrEmpty(zipCode) || string.IsNullOrEmpty(houseNumber))
                throw new NullReferenceException("Zipcode and housenumber cannot be null");
            
            zipCode = zipCode.Replace(" ", "").ToUpper();
            houseNumber = houseNumber.Trim();

            if (houseNumberExtension != string.Empty)
                houseNumberExtension = houseNumberExtension.Trim().ToLower();

            return _validAddressList.FirstOrDefault(a => a.ZipCode == zipCode && a.HouseNumber == houseNumber && a.HouseNumberExtension == houseNumberExtension);   
        }

        /// <summary>
        /// Get the full address based on the given addresses' zipcode, housenumber and optionally housenumberextension
        /// </summary>
        /// <param name="address">The Address to fill the details on</param>
        /// <returns>Full address based on the given address, or null if not found in _validAddressList</returns>
        public Address GetFullAddress(Address address)
        {
            if (address == null || string.IsNullOrEmpty(address.ZipCode) || string.IsNullOrEmpty(address.HouseNumber))
                throw new NullReferenceException("Address model, Zipcode and Housenumber cannot be null");

            if (address.HouseNumberExtension == null)
                address.HouseNumberExtension = string.Empty;

            address.HouseNumber.Trim();
            address.HouseNumberExtension.Trim();

            Address fullAddress = _validAddressList.FirstOrDefault(a => 
                a.ZipCode == address.ZipCode && 
                a.HouseNumber == address.HouseNumber && 
                a.HouseNumberExtension == address.HouseNumberExtension
            );
            return fullAddress;
        }
    }
}
