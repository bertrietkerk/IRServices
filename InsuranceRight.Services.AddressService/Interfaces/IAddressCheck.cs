using InsuranceRight.Services.Shared.Models;

namespace InsuranceRight.Services.AddressService.Interfaces
{
    public interface IAddressCheck
    {
        bool IsZipCodeValid(string zipCode);
        Address GetFullAddress(string zipCode, string houseNumber, string houseNumberExtension = "");
    }
}