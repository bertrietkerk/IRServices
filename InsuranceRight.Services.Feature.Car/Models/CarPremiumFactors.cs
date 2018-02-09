using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class CarPremiumFactors
    {
        public CarPremiumFactors()
        {
            Car = new CarObject();
            Driver = new MostFrequentDriverViewModel()
            {
                ResidenceAddress = new Address(),
                CorrespondenceAddress = new Address()
            };
        }

        public virtual string GroupDiscountCode { get; set; }
        public virtual string RegularDriver { get; set; }
        public virtual CarObject Car { get; set; }
        public virtual MostFrequentDriverViewModel Driver { get; set; }
    }
}
