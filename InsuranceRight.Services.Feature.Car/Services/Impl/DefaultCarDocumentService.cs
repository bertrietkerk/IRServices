using System.Collections.Generic;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Foundation;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarDocumentService : ICarDocumentService
    {
        public IEnumerable<PolicyDocument> GetDocuments(CarViewModel model)
        {
            List<PolicyDocument> result = null;
            
            if (model != null)
            {
                result =  new List<PolicyDocument>()
                {
                    new PolicyDocument()
                    {
                        Id = 1,
                        Title = "Car Policy",
                        IconUrl = "/-/media/brands/yourbrand/Images/Content Images/policy.png",
                        DocumentUrl = "/-/media/brands/yourbrand/files/policy/car-policy-paper.pdf",
                        FileSize = "1 MB"
                    },
                    new PolicyDocument()
                    {
                        Id = 2,
                        Title = "White and Green Card",
                        IconUrl = "/-/media/brands/yourbrand/Images/Content Images/policy_green.png",
                        DocumentUrl = "/-/media/brands/yourbrand/files/policy/white-and-green-card.pdf",
                        FileSize = "2 MB"
                    }
                };
            }

            return result;
        }
    }
}
