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
            
            //Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.CompassCardTitle)), 
            //    homePage.CompassCardTitleFailMsg);
            //Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.CompassCardDescription)), 
            //    homePage.CompassCardDescriptionFailMsg);
            //homePage.GoToCompassCardCard();
            //Assert.IsTrue(driver.Url.Contains("compasscard"), "Compass Card is Not Displayed");

            //homePage.DriverSwitchBackToHomePage();
                                   
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.TransitAlertsCardDescription)), 
                homePage.TransitAlertsCardDescriptionFailMsg);
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(homePage.TransitAlertsCardTitle)),
                homePage.TransitAlertsCardTitleFailMsg);
     
            homePage.ClickFaresHamMenu();
            homePage.GoBackToHomePage();

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
