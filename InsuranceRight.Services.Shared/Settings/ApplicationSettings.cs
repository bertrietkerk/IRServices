using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Settings
{
    public class ApplicationSettings
    {
        public virtual AcceptanceSettings AcceptanceSettings { get; set; }
        public virtual PremiumCalculationSettings PremiumCalculationSettings { get; set; }
    }
}
