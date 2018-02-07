using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Models.Settings;
using Microsoft.Extensions.Options;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarDiscountPolicy : ICarDiscountPolicy
    {
        private readonly IOptions<DiscountSettings> _settings;

        public DefaultCarDiscountPolicy(IOptions<DiscountSettings> settings)
        {
            _settings = settings;
        }


        public CarDiscountPolicy GetDiscountForGroup(string code)
        {
            CarDiscountPolicy response = new CarDiscountPolicy() { Code = code, IsDiscountFound = false, Amount = 0 };

            if (!string.IsNullOrWhiteSpace(code) && _settings.Value.DiscountCodes.TryGetValue(code, out int amount))
            {
                response.IsDiscountFound = true;
                response.Amount = amount;
            }

            return response;
        }
    }
}
