using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class FaresTest : UITestFixture
    {
        [TestCase()]
        //[TestCase()]
        public void FaresPageVerification()
        {
            FaresPage farePage = new FaresPage(driver);
            farePage.ClickFaresLink();

            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  "Learn about the fare types, prices, and where to buy.",
                                  "This is Not the Fares Page");
        }


        /*[TestCase(), Order(2), Category("Smoke")]
        public void FareZoneContainersVerify()
        {
            FaresPage farePage = new FaresPage(driver);
            farePage.ClickFaresLink();

            farePage.ClickPriceFareZones();
            Assert.Contains(driver.FindElement(By.TagName("body")).Text.Contains("There are three fare zones " +
                "across Metro Vancouver. The number of SeaBus and/or SkyTrain boundaries you cross during your " +
                "trip determine your fare."), "Fare Description is Incorrect");
            farePage.BackToFaresPage();

            farePage.ClickCompassCard();
            Assert.Contains(driver.FindElement(By.TagName("body")).Text.Contains("We offer a range of fares, passes, " +
                "and ticket types, to reflect the different ways you can get around."),
                "Compass Card Description is Incorrect");
            farePage.BackToFaresPage();

        }*/
    }
}
