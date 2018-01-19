using InsuranceRight.Services.Feature.Car.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRight.Services.Feature.Car.Services.Impl
{
    public class DefaultCarLookup : ICarLookup
    {
        private readonly ICarDataProvider _carData;

        public DefaultCarLookup(ICarDataProvider carDataProvider)
        {
            _carData = carDataProvider;
        }

        public List<string> GetBrands()
        {
            var brands = new List<string>();
            var cars = _carData.GetCars();
            if (cars != null && cars.Count() > 0)
            {
                foreach (var c in cars)
                {
                    if (!brands.Contains(c.Brand))
                    {
                        brands.Add(c.Brand);
                    }
                }
                return brands;
            }
            //null if no car was found
            return null;
        }

        public List<string> GetModels(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                return null;
            }

            var cars = _carData.GetCars().Where(c => c.Brand == brand.ToUpper().Trim());
            if (cars != null && cars.Count() > 0)
            {
                List<string> models = new List<string>();
                foreach (var c in cars)
                {
                    if (!models.Contains(c.Model))
                    {
                        models.Add(c.Model);
                    }
                }
                return models;
            }
            // return null if no car by that brand was found
            return null;
        }


        public List<string> GetEditions(string brand, string model)
        {
            if (string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(model))
            {
                return null;
            }

            var cars = _carData.GetCars().Where(c => c.Brand == brand.ToUpper().Trim() && c.Model == model.ToUpper().Trim());
            if (cars != null && cars.Count() > 0)
            {
                List<string> editions = new List<string>();
                foreach (var c in cars)
                {
                    if (!editions.Contains(c.Edition))
                    {
                        editions.Add(c.Edition);
                    }
                }
                return editions;
            }
            return null;

        }


        public decimal GetWeight(string brand, string model, string edition)
        {
            decimal weight = 0;
            if (!string.IsNullOrWhiteSpace(brand) && !string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(edition))
            {
                var car = _carData.GetCars().FirstOrDefault(c =>
                    c.Brand == brand.ToUpper().Trim() &&
                    c.Model == model.ToUpper().Trim() &&
                    c.Edition == edition.ToUpper().Trim()
                );

                if (car != null)
                {
                    decimal.TryParse(car.Weight, out weight);
                }
            }
            return weight;
        }

        public decimal GetCatalogValue(string brand, string model, string edition)
        {
            decimal catalogValue = 0;
            if (!string.IsNullOrWhiteSpace(brand) && !string.IsNullOrWhiteSpace(model) && !string.IsNullOrWhiteSpace(edition))
            {
                var car = _carData.GetCars().FirstOrDefault(c =>
                    c.Brand == brand.ToUpper().Trim() &&
                    c.Model == model.ToUpper().Trim() &&
                    c.Edition == edition.ToUpper().Trim()
                );

                if (car != null)
                {
                    catalogValue = car.Price.CatalogPrice;
                }
            }
            return catalogValue;
        }
    }
}
