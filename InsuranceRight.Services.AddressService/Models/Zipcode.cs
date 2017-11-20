namespace InsuranceRight.Services.AddressService.Models
{
    public class ZipCode
    {
        public string Zipcode { get; set; }
        public override string ToString()
        {
            return Zipcode;
        }  
    }
}