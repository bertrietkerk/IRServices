using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.AddressService.Interfaces
{
    public interface IAddressLookup
    {
        bool IsZipCodeValid(string zipCode);
        Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension = "");
    }
}