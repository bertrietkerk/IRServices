using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Foundation;
using System.Collections.Generic;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarDocumentService
    {
        IEnumerable<PolicyDocument> GetDocuments(CarViewModel model);
    }
}
