using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Response;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarDiscountPolicy : ICarDiscountPolicy
    {
        public ReturnObject<CarDiscountPolicy> GetDiscountForGroup(string code)
        {
            ReturnObject<CarDiscountPolicy> response = new ReturnObject<CarDiscountPolicy>();
            response.Object = new CarDiscountPolicy() { Code = code, Amount = 0M, IsDiscountFound = false };

            var validCodes = GetValidCodes();
            if (string.IsNullOrWhiteSpace(code) || !validCodes.ContainsKey(code))
                response.ErrorMessages.Add("Discount code was not found"); 
            else
            {
                response.Object.IsDiscountFound = true;
                validCodes.TryGetValue(code, out decimal amount);
                response.Object.Amount = amount;
            }
            return response;
        }

        private Dictionary<string, decimal> GetValidCodes()
        {
            return new Dictionary<string, decimal>()
            {
                { "VA", 1M },
                { "TMG", 5M },
                { "InsuranceRight", 100M }
            };
        }
    }
}
