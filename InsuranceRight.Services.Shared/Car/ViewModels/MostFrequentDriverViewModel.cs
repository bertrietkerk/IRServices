using InsuranceRight.Services.Models.Enums;
using InsuranceRight.Services.Models.Foundation;
using InsuranceRight.Services.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Models.Car.ViewModels
{
    public class MostFrequentDriverViewModel : PersonViewModel
    {
        public virtual string Age { get; set; }
        public virtual bool? HadDamageOrTheftBefore{ get; set; }
        public virtual string DamageFreeYears { get; set; }
        public virtual KilometersPerYear KilometersPerYear { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }

        public override Address ResidenceAddress { get; set; }
        public override bool IsCorrespondenceAddressDifferent { get; set; }
        public override Address CorrespondenceAddress { get ; set; }

    }
}
