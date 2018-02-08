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

        public CarAcceptanceRepositoryTests()
        {
            _sut = new DefaultCarAcceptance(Options.Create(new AcceptanceSettings()));
        }

        [TestMethod]
        public void Check__EmptyCarAndDriver__Returns__()
        {
            //var driver = new MostFrequentDriverViewModel();
            //var car = new CarObject();
            //var result = _sut.Check(driver, car);

            //Assert.IsFalse(result.IsAccepted);
            //Assert.AreEqual("Alarm/Immobilizer/Mechanical security/Satellite monitoring is/are null", result.Reason);
        }

        [TestMethod]
        public void Check__CarSecurity__Returns__()
        {

        }

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
