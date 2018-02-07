using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Settings
{
    public class AcceptanceSettings
    {
        public virtual int ExpensiveCarBoundary { get; set; }
        public virtual bool AcceptAlways { get; set; }
    }
}
