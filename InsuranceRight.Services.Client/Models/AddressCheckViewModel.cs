using InsuranceRight.Services.Models.Response;
using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.Client.Models
{
    public class AddressCheckViewModel
    {
        public Address Address { get; set; }
        public ReturnObject<ZipCode> ReturnZipCodeObject { get; set; }
        public ReturnObject<Address> ReturnAddressObject { get; set; }

    }
}