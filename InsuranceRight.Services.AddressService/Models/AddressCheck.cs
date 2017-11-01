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
        // Constants (injected in ctor based on json files in .\Json folder)
        public readonly List<string> _validZipCodeList;
        public readonly List<Address> _validAddressList;
        private static readonly string _defaultDutchRegexPattern = "^[1-9][0-9]{3}[A-Z]{2}$";

        public AddressCheck()
        {
            _validAddressList = GetValidAddresses();
            _validZipCodeList = GetValidZipdCodes();
        }

        #region GetData

        /// <summary>
        /// Gets all addresses from the json files in Json folder, and adds them to _validAddressList 
        /// </summary>
        /// <returns>List of valid Addresses</returns>
        public List<Address> GetValidAddresses()
        {
            var list = new List<Address>();
            Address address;
            string[] files = Directory.GetFiles(@".\Json");
            foreach (String file in files)
            {
                using (StreamReader sr = File.OpenText(file))
                {
                    JsonSerializer ser = new JsonSerializer();
                    address = (Address)ser.Deserialize(sr, typeof(Address));
                    if (address != null)
                    {
                        list.Add(address);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get all valid ZipCodes based on all the addresses in the _validAddressList 
        /// </summary>
        /// <returns>List of valid zipcodes (strings)</returns>
        private List<string> GetValidZipdCodes()
        {
            var list = new List<string>();
            foreach (Address address in _validAddressList)
            {
                if (!list.Contains(address.ZipCode))
                {
                    list.Add(address.ZipCode);
                }
            }
            return list;
        }

        #endregion

        ////////////////////
        // Validate ZipCode
        ////////////////////

        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match default 'Dutch Zipcode' regex pattern </returns>
        public bool ValidateZipCode(string zipCode)
        {
            return ValidateZipCode(zipCode, _defaultDutchRegexPattern);
        }

        /// <summary>
        /// Checks if zipCode is valid, based on _validZipCodes list
        /// </summary>
        /// <param name="zipCode">The zipcode to check</param>
        /// <param name="pattern">An optional custom regex pattern which the zipcode must conform to in order to be checked</param>
        /// <returns>True: zipcode is valid || False: if invalid or doesn't match regex pattern </returns>
        public bool ValidateZipCode(string zipCode, string pattern)
        {
            zipCode.Replace(" ", "").ToUpper();
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(zipCode))
            {
                return false;
            }
            return _validZipCodeList.Contains(zipCode);
        }

        ////////////////////
        // Get Full Address
        ////////////////////

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
            return _validAddressList.FirstOrDefault(a => a.ZipCode == zipCode && a.HouseNumber == houseNumber && a.HouseNumberExtension == houseNumberExtension);   
        }

        /// <summary>
        /// Get the full address based on the given addresses' zipcode, housenumber and optionally housenumberextension
        /// </summary>
        /// <param name="address">The Address to fill the details on</param>
        /// <returns>Full address based on the given address, or null if not found in _validAddressList</returns>
        public Address GetFullAddress(Address address)
        {
            if (address.HouseNumberExtension == null)
            {
                address.HouseNumberExtension = string.Empty;
            }

            Address fullAddress = _validAddressList.FirstOrDefault(a => 
                a.ZipCode == address.ZipCode && 
                a.HouseNumber == address.HouseNumber && 
                a.HouseNumberExtension == address.HouseNumberExtension
            );
            return fullAddress;
        }
    }
}
