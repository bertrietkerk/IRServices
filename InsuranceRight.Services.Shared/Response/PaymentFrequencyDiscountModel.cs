using InsuranceRight.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Response
{
    public class PaymentFrequencyDiscountModel
    {
        public PaymentFrequency Frequency { get; set; }
        public decimal Amount { get; set; }
    }
}
