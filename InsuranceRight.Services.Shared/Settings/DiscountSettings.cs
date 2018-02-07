using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Settings
{
    public class DiscountSettings
    {
        public virtual Dictionary<string, int> DiscountCodes { get; set; }
    }
}
