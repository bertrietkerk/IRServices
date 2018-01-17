using InsuranceRight.Services.Feature.Car.Models;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarDiscountPolicy
    {
        CarDiscountPolicy GetDiscountForGroup(string code);
    }
}
