using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Acceptance;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarAcceptance
    {
        AcceptanceStatus Check(MostFrequentDriverViewModel driver, CarObject car);
        //AcceptanceStatus IsCarSecurityAccepted(decimal catalogPrice, bool hasImmobilizer, bool hasAlarm, bool hasMechanicalSecurity, bool hasSatMonitoring);
        //AcceptanceStatus IsMostFrequentDriverAccepted(MostFrequentDriverViewModel driver, CarObject car);
        //AcceptanceStatus IsRiskAssesmentAccepted(MostFrequentDriverViewModel driver, CarObject car);
    }
}
