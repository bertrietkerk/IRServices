using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarDiscountPolicy
    {
        ReturnObject<CarDiscountPolicy> GetDiscountForGroup(string code);
    }
}
