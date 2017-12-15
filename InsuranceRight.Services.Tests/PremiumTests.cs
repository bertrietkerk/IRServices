using InsuranceRight.Services.Feature.Car.Services.Impl;
using InsuranceRight.Services.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsuranceRight.Services.Tests
{
    [TestClass]
    public class PremiumTests
    {
        private readonly DefaultPremiumCalculator _premiumCalc;


        public PremiumTests()
        {
            _premiumCalc = new DefaultPremiumCalculator();
        }



        [TestMethod]
        public void CalculateMtplPremium__DifferentAgeRanges__ReturnsDifferentPremiumCosts()
        {
            var variant1 = _premiumCalc.CalculateMtplPremium(1, "18-24", "1", "3013AA", KilometersPerYear.Between10000and15000);
            var variant2 = _premiumCalc.CalculateMtplPremium(1, "25-39", "1", "3013AA", KilometersPerYear.Between10000and15000);

            var variant3 = _premiumCalc.CalculateMtplPremium(1, "40-64", "1", "3013AA", KilometersPerYear.Between10000and15000);
            var variant4 = _premiumCalc.CalculateMtplPremium(1, "65 +", "1", "3013AA", KilometersPerYear.Between10000and15000);

            Assert.IsTrue(variant1.Premium > variant2.Premium);
            Assert.IsTrue(variant3.Premium < variant4.Premium);

            Assert.IsTrue(variant1.Premium >= 150 && variant1.Premium <= 200);
            Assert.IsTrue(variant2.Premium >= 120 && variant2.Premium <= 150);
            Assert.IsTrue(variant3.Premium >= 120 && variant3.Premium <= 150);
            Assert.IsTrue(variant4.Premium >= 140 && variant4.Premium <= 180);
        }
    }
}
