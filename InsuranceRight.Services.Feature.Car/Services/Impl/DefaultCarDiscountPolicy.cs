using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Response;
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
            int amount = 0;

            if (!string.IsNullOrWhiteSpace(code) && _settings.Value.DiscountCodes.TryGetValue(code, out amount))
            {
                response.IsDiscountFound = true;
                response.Amount = amount;
            }

            return response;
        }
    }
}
