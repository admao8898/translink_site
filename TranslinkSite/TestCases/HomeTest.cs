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
        public void HomePageContents()
        {
            HomePage homePage = new HomePage(driver);
            
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.CompassCardTitle)), homePage.CompassCardTitleErrorMsg);
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.CompassCardDescription)), homePage.CompassCardDescriptionErrorMsg);
            homePage.GoToCompassCardCard();
            Assert.IsTrue(driver.Url.Contains("compasscard"), "Compass Card is Not Displayed");

            homePage.DriverSwitchBackToHomePage();
                       
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.TransitAlertsCardDescription)), homePage.TransitAlertsCardDescriptionErrorMsg);
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.TransitAlertsCardTitle)), homePage.TransitAlertsCardTitleErrorMsg);
            homePage.GoToTransitAlerts();
            homePage.GoBackToHomePage(); 

            homePage.GoToContactUs();
            Assert.IsTrue(driver.Url.Contains("more-information/contact-information"));
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
