﻿using InsuranceRight.Services.Acceptance.Services.Impl;
using InsuranceRight.Services.Feature.Car.Models;
using InsuranceRight.Services.Feature.Car.Models.ViewModels;
using InsuranceRight.Services.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InsuranceRight.Services.Feature.Car.Tests.Impl_Tests
{
    [TestClass]
    public class CarAcceptanceRepositoryTests
    {
        private readonly DefaultCarAcceptance _sut;
        private readonly int ExpensiveCarBoundary;
        private readonly MostFrequentDriverViewModel LessThan18DriverModel;
        private readonly MostFrequentDriverViewModel NineteenYearOldDriverModel;
        private readonly MostFrequentDriverViewModel CorrectDriverModel;
        private readonly CarObject CorrectCar;
        private readonly CarObject ExpensiveCar;



        public CarAcceptanceRepositoryTests()
        {
            // Variables setup
            ExpensiveCarBoundary = 5000;
            ExpensiveCar = new CarObject() { Price = new CarPrice() { CatalogPrice = ExpensiveCarBoundary * 2 } };
            CorrectCar = new CarObject() { Price = new CarPrice() { CatalogPrice = ExpensiveCarBoundary / 2 } };
            LessThan18DriverModel = new MostFrequentDriverViewModel() { BirthDate = DateTime.Now };
            NineteenYearOldDriverModel = new MostFrequentDriverViewModel() { BirthDate = DateTime.Now.AddYears(-19) };
            CorrectDriverModel = new MostFrequentDriverViewModel() { BirthDate = new DateTime(1993, 1, 1) };

            // SUT setup
            _sut = new DefaultCarAcceptance(Options.Create(new AcceptanceSettings() { AcceptAlways = false, ExpensiveCarBoundary = ExpensiveCarBoundary }));
        }

        // no check for null input to Check(null), since null check is caught in controller 


        #region SecurityMeasurements Check tests

        // exp car without security and normal driver = not accepted
        [TestMethod]
        public void Check__ExpensiveCarWithoutSecurity__ReturnsNotAccepted()
        {
            var result = _sut.Check(CorrectDriverModel, ExpensiveCar);

            Assert.IsFalse(result.IsAccepted);
        }

        #endregion

        // exp car with security and driver = accept
        [TestMethod]
        public void Check__ExpensiveCarWithSecurity__ReturnsAccepted()
        {
            var securedCar = ExpensiveCar;
            securedCar.Alarm = true;
            var result = _sut.Check(CorrectDriverModel, securedCar);

            Assert.IsTrue(result.IsAccepted);
        }

        // exp car with security and young driver = not accept
        [TestMethod]
        public void Check__ExpensiveCarWithSecurity19YearOldDriver__ReturnsNotAccepted()
        {
            var securedCar = ExpensiveCar;
            securedCar.Alarm = true;
            var result = _sut.Check(NineteenYearOldDriverModel, securedCar);

            Assert.IsFalse(result.IsAccepted);
        }

        // normal car and young driver = accept
        [TestMethod]
        public void Check__NormalCarWithoutSecurityYoungDriver__ReturnsAccepted()
        {
            var result = _sut.Check(NineteenYearOldDriverModel, CorrectCar);

            Assert.IsTrue(result.IsAccepted);
        }

        // how extensive?

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
