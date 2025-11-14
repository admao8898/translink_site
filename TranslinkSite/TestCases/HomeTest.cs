using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Locators;
using TranslinkSite.Pages;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;


namespace TranslinkSite.TestCases
{
    public class HomeTest : UITestFixture
    {
        [TestCase(), Category("Smoke"), Order(1)]
        public void HomePageContentsVerification()
        {
            HomePage homePage = new HomePage(driver);
            HomePageLocators homePageLocators = new HomePageLocators();

            Assert.Contains(driver.FindElement(By.TagName("body")).Text.Contains(homePageLocators.TranslinkTitle),
                homePageLocators.TranslinkTitleErrorMsg);
            Assert.Contains(driver.FindElement(By.TagName("body")).Text.Contains(homePageLocators.TranslinkDescript),
                homePageLocators.TranslinkDescriptErrorMsg);
     
            homePage.ClickFaresHamMenu();
            homePage.GoBackToHomePage();

            homePage.GoToTransitAlerts();
            //homePage.DriverSwitchBackToHomePage();
            homePage.GoBackToHomePage();

            homePage.GoToContactUs();
            Assert.Contains("contact-information", driver.Url, "URL should contain 'contact-information'");
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToFares();
            Assert.Contains("transit-fares", driver.Url, "URL should contain 'transit-fares'");
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToRiderInfo();
            Assert.Contains("rider-guide", driver.Url, "URL should contain 'rider-guide'");
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToSchedules();
            Assert.Contains("schedules-and-maps", driver.Url, "URL should contain 'schedules-and-maps'");
        }
    }
}
