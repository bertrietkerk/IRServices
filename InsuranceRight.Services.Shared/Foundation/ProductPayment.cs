using InsuranceRight.Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Foundation
{
    public class ProductPayment
    {
        public virtual PaymentFrequency PaymentFrequency { get; set; }
    }
}
