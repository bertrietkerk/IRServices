using InsuranceRight.Services.Models.Car.ViewModels;
using InsuranceRight.Services.Models.Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarDocumentService
    {
        IEnumerable<PolicyDocument> GetDocuments(CarViewModel model);
    }
}
