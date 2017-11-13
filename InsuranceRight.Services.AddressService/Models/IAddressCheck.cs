using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.AddressService.Models
{
    public interface IAddressCheck
    {

        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match default 'Dutch Zipcode' regex pattern </returns>
        bool IsZipCodeValid(string zipCode);

        Address GetFullAddress(string zipCode, string houseNumber);

        
        /// <summary>
        /// Get the full address based on the zipcode, housenumber and housenumberextension
        /// </summary>
        /// <param name="zipCode">The zipcode of the full address</param>
        /// <param name="houseNumber">The housenumber of the full address</param>
        /// <param name="houseNumberExt">The housenumberextension of the full address</param>
        /// <returns>The full address based on zipcode, housenumber & housenumberextension, or null if not found in _validAddressList</returns>
        Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension);

        /// <summary>
        /// Get the full address based on the given addresses' zipcode, housenumber and optionally housenumberextension
        /// </summary>
        /// <param name="address">The Address to fill the details on</param>
        /// <returns>Full address based on the given address, or null if not found in _validAddressList</returns>
        Address GetFullAddress(Address address);
    }
}