using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Settings
{
    public class ApplicationSettings
    {
        public AcceptanceSettings AcceptanceSettings { get; set; }
        public PremiumCalculationSettings PremiumCalculationSettings { get; set; }
    }
}
