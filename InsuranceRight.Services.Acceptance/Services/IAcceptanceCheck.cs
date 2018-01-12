using InsuranceRight.Services.Models.Acceptance;
using InsuranceRight.Services.Models.Car;
using InsuranceRight.Services.Models.Car.ViewModels;
using InsuranceRight.Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Acceptance.Services
{
    public interface IAcceptanceCheck
    {
        AcceptanceStatus Check(MostFrequentDriverViewModel driver, CarObject car);
        //AcceptanceStatus IsCarSecurityAccepted(decimal catalogPrice, bool hasImmobilizer, bool hasAlarm, bool hasMechanicalSecurity, bool hasSatMonitoring);
        //AcceptanceStatus IsMostFrequentDriverAccepted(MostFrequentDriverViewModel driver, CarObject car);
        //AcceptanceStatus IsRiskAssesmentAccepted(MostFrequentDriverViewModel driver, CarObject car);
    }
}
