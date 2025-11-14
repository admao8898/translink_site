using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Locators;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class HomeTest : UITestFixture
    {
        [TestCase(), Category("Smoke"), Order(1)]
        public void HomePageContentsVerification()
        {
            HomePage homePage = new HomePage(driver);
            HomePageLocators homePageLocators = new HomePageLocators();

            // Verify page text
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  homePageLocators.TranslinkTitle,
                                  homePageLocators.TranslinkTitleErrorMsg);

            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  homePageLocators.TranslinkDescript,
                                  homePageLocators.TranslinkDescriptErrorMsg);

            // Navigation and URL checks
            homePage.ClickFaresHamMenu();
            homePage.GoBackToHomePage();

            homePage.GoToTransitAlerts();
            homePage.GoBackToHomePage();

            homePage.GoToContactUs();
            StringAssert.Contains(driver.Url,
                                  "contact-information",
                                  "URL should contain 'contact-information'");
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToFares();
            StringAssert.Contains(driver.Url,
                                  "transit-fares",
                                  "URL should contain 'transit-fares'");
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToRiderInfo();
            StringAssert.Contains(driver.Url,
                                  "rider-guide",
                                  "URL should contain 'rider-guide'");
            homePage.ClickTranslinkHomePageLogo();

            homePage.GoToSchedules();
            StringAssert.Contains(driver.Url,
                                  "schedules-and-maps",
                                  "URL should contain 'schedules-and-maps'");
        }

    }
}
