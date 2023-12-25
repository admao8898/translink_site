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

            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(homePageLocators.TranslinkTitle),
                homePageLocators.TranslinkTitleErrorMsg);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(homePageLocators.TranslinkDescript),
                homePageLocators.TranslinkDescriptErrorMsg);
     
            homePage.ClickFaresHamMenu();
            homePage.GoBackToHomePage();

            homePage.GoToTransitAlerts();
            //homePage.DriverSwitchBackToHomePage();
            homePage.GoBackToHomePage();

            homePage.GoToContactUs();
            Assert.IsTrue(driver.Url.Contains("contact-information"));
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToFares();
            Assert.IsTrue(driver.Url.Contains("transit-fares"));
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToRiderInfo();
            Assert.IsTrue(driver.Url.Contains("rider-guide"));
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToSchedules();
            Assert.IsTrue(driver.Url.Contains("schedules-and-maps"));
        }
    }
}
