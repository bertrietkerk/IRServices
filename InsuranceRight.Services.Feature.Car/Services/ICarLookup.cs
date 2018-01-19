using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services
{
    public interface ICarLookup
    {
        List<string> GetBrands();

        List<string> GetModels(string brand);

        List<string> GetEditions(string brand, string model);

        decimal GetWeight(string brand, string model, string edition);

        decimal GetCatalogValue(string brand, string model, string edition);

    }
}
