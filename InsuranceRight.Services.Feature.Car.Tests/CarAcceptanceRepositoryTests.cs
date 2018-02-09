using InsuranceRight.Services.Acceptance.Services.Impl;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceRight.Services.Feature.Car.Tests
{
    [TestClass]
    public class CarAcceptanceRepositoryTests
    {
        private readonly DefaultCarAcceptance _sut;
        private readonly int ExpensiveCarBoundary;
        private readonly MostFrequentDriverViewModel YoungDriverModel;
        private readonly MostFrequentDriverViewModel CorrectDriverModel;
        private readonly CarObject CorrectCar;
        private readonly CarObject ExpensiveCar;



        public CarAcceptanceRepositoryTests()
        {
            // Variables setup
            ExpensiveCarBoundary = 5000;
            ExpensiveCar = new CarObject() { Price = new CarPrice() { CatalogPrice = ExpensiveCarBoundary * 2 } };
            CorrectCar = new CarObject() { Price = new CarPrice() { CatalogPrice = ExpensiveCarBoundary / 2 } };
            YoungDriverModel = new MostFrequentDriverViewModel() { BirthDate = DateTime.Now };
            CorrectDriverModel = new MostFrequentDriverViewModel() { BirthDate = new DateTime(1993, 1, 1) };

            // SUT setup
            _sut = new DefaultCarAcceptance(Options.Create(new AcceptanceSettings() { AcceptAlways = false, ExpensiveCarBoundary = ExpensiveCarBoundary }));
        }

        //[TestMethod]
        //public void Check__EmptyCarAndDriver__Returns__()
        //{
        //    var driver = new MostFrequentDriverViewModel();
        //    var car = new CarObject();
        //    var result = _sut.Check(driver, car);

        //    Assert.IsFalse(result.IsAccepted);
        //}

        [TestMethod]
        public void Check__ExpensiveCarWithoutSecurity__ReturnsNotAccepted()
        {
            var result = _sut.Check(CorrectDriverModel, ExpensiveCar);

            Assert.IsFalse(result.IsAccepted);
        }
        // exp car with security and driver = accept
        // exp car with security and young driver = not accept
        // normal car and young driver = accept


        [TestMethod]
        public void Check__DataMostFrequentDriver__Returns__()
        {

        }

        [TestMethod]
        public void Check__DataRiskAssesment__Returns__()
        {

        }
    }
}
