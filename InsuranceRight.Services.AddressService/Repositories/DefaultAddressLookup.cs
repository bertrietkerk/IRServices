using System;
using System.Collections.Generic;
using System.Linq;
using InsuranceRight.Services.AddressService.Interfaces;
using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.AddressService.Repositories
{
    public class DefaultAddressLookup : IAddressLookup
    {
        
        private readonly IEnumerable<String> _validZipCodeList;
        private readonly IEnumerable<Address> _validAddressList;

        public DefaultAddressLookup(IAddressDataProvider dataProvider)
        {
            _validAddressList = dataProvider.GetValidAddresses();
            _validZipCodeList = dataProvider.GetValidZipCodes(_validAddressList);
        }

        
        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match regex pattern </returns>
        public bool IsZipCodeValid(string zipCode)
        {
            return _validZipCodeList.Contains(zipCode);
        }

        /// <summary>
        /// Get the full address based on the zipcode, housenumber and housenumberextension
        /// </summary>
        /// <param name="zipCode">The zipcode of the full address</param>
        /// <param name="houseNumber">The housenumber of the full address</param>
        /// <param name="houseNumberExtension">The housenumberextension of the full address</param>
        /// <returns>The full address based on zipcode, housenumber and housenumberextension, or null if not found in _validAddressList</returns>
        public Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension = "")
        {
            return _validAddressList.FirstOrDefault(a => a.ZipCode == zipCode && a.HouseNumber == houseNumber && a.HouseNumberExtension == houseNumberExtension);   
        }
    }
}
