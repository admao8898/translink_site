using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class FaresTest : UITestFixture
    {
        [TestCase(), Order(1)]
        //[TestCase()]
        public void FaresPageVerification()
        {
            FaresPage farePage = new FaresPage(driver);
            farePage.ClickFaresLink();
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.
                Contains("Learn about the fare types, prices, and where to buy."),
                "This is Not the Fares Page");
        }

        [TestCase(), Order(2), Category("Smoke")]
        public void FareZoneContainersVerify()
        {
            FaresPage farePage = new FaresPage(driver);
            farePage.ClickFaresLink();

            farePage.ClickPriceFareZones();
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("There are three fare zones " +
                "across Metro Vancouver. The number of SeaBus and/or SkyTrain boundaries you cross during your " +
                "trip determine your fare."), "Fare Description is Incorrect");
            farePage.BackToFaresPage();

            farePage.ClickCompassCard();
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("We offer a range of fares, passes, " +
                "and ticket types, to reflect the different ways you can get around."),
                "Compass Card Description is Incorrect");
            farePage.BackToFaresPage();

        }
    }
}
