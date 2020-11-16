using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages; 

namespace TranslinkSite.TestCases
{
    public class HomeTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void HomePageContentsVerification()
        {
            HomePage homePage = new HomePage(driver);          
                                   
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.TranslinkTitle)), 
                homePage.TranslinkTitleErrorMsg);
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.TranslinkDescript)),
                homePage.TranslinkDescriptErrorMsg);
     
            homePage.ClickFaresHamMenu();
            homePage.GoBackToHomePage();

            homePage.GoToTransitAlerts();
            homePage.DriverSwitchBackToHomePage();

            homePage.GoToContactUs();
            Assert.IsTrue(driver.Url.Contains("contact-information"));
            homePage.GoBackToHomePage();

            homePage.GoToFares();
            Assert.IsTrue(driver.Url.Contains("transit-fares"));
            homePage.GoBackToHomePage();

            homePage.GoToRiderInfo();
            Assert.IsTrue(driver.Url.Contains("rider-guide"));
            homePage.GoBackToHomePage();

            homePage.GoToSchedules();
            Assert.IsTrue(driver.Url.Contains("schedules-and-maps"));
        }
    }
}
