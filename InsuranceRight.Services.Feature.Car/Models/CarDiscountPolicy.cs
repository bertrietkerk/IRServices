﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Models
{
    public class CarDiscountPolicy
    {
        public bool IsDiscountFound { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
    }
}
