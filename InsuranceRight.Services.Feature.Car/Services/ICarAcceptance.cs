using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Acceptance;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarAcceptance
    {
        AcceptanceStatus Check(MostFrequentDriverViewModel driver, CarObject car);
    }
}
