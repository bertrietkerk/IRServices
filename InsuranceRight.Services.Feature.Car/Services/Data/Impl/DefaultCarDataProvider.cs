using System.Collections.Generic;
using InsuranceRight.Services.Models.Car;
using System.IO;
using Newtonsoft.Json;
using InsuranceRight.Services.Models.JsonRootObjects;

namespace InsuranceRight.Services.Feature.Car.Services.Data.Impl
{
    public class DefaultCarDataProvider : ICarDataProvider
    {
        public List<CarObject> GetCars()
        {
            List<CarObject> carsList = new List<CarObject>();
            CarJsonRootObject rootObject;


            // using (StreamReader sr = File.OpenText(@".\Services\Data\Json\cars.json"))
            using (StreamReader sr = File.OpenText(@"C:\Projects\InsuranceRight.Services\InsuranceRight.Services.Feature.Car\Services\Data\Json\cars.json"))
            {
                JsonSerializer ser = new JsonSerializer();
                rootObject = (CarJsonRootObject)ser.Deserialize(sr, typeof(CarJsonRootObject));
                if (rootObject != null)
                {
                    foreach (CarObject car in rootObject.Cars)
                    {
                        carsList.Add(car);
                    }
                }
            }
            return carsList;
        }



        public List<CarObject> GetHardCodedCars()
        {
            return new List<CarObject>(new[]
            {
                new CarObject
                {
                    LicensePlate = "ST-IG-13",
                    Brand = "AUDI",
                    Model = "TT",
                    Fuel = "BENZINE",
                    GearType = "Manual",
                    Weight = "2510",
                    Edition = "1.8 T",
                    Year = "2012",
                    Price = new CarPrice
                    {
                        CatalogPrice = 20250,
                        CurrentPrice = 15000
                    },
                    IsVehicleFound = true
                },
                new CarObject
                {
                    LicensePlate = "01-GBB-1",
                    Brand = "VOLVO",
                    Model = "XC60",
                    Fuel = "DIESEL",
                    GearType = "Manual",
                    Weight = "2400",
                    Edition = "2.0",
                    Year = "2015",
                    Price = new CarPrice
                    {
                        CatalogPrice = 18000,
                        CurrentPrice = 17000
                    },
                    IsVehicleFound = true
                },
                new CarObject
                {
                    LicensePlate = "SK-595-AA",
                    Brand = "NISSAN",
                    Model = "SUNNY",
                    Fuel = "DIESEL",
                    GearType = "Manual",
                    Weight = "2420",
                    Edition = "1.4 LX",
                    Year = "2014",
                    Price = new CarPrice
                    {
                        CatalogPrice = 16000,
                        CurrentPrice = 12000
                    },
                    IsVehicleFound = true
                },
                new CarObject
                {
                    LicensePlate = "01-AWW-1",
                    Brand = "JEEP",
                    Model = "LAND ROVER DEFENDER",
                    Fuel = "Benzin",
                    GearType = "Manual",
                    Weight = "2900",
                    Edition = "4WD",
                    Year = "1983",
                    Price = new CarPrice
                    {
                        CatalogPrice = 13000,
                        CurrentPrice = 10000
                    },
                    IsVehicleFound = true
                },
                new CarObject
                {
                    LicensePlate = "01-QWE-1",
                    Brand = "RENAULT",
                    Model = "CILO",
                    Fuel = "Benzin",
                    GearType = "Manual",
                    Weight = "2400",
                    Edition = "Authentique",
                    Year = "2006",
                    Price = new CarPrice
                    {
                        CatalogPrice = 18000,
                        CurrentPrice = 13000
                    },
                    IsVehicleFound = true
                }
            });
        }
    }
}
