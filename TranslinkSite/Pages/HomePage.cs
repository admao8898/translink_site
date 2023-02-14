using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.Locators;
using TranslinkSite.Pages;

namespace TranslinkSite.Pages
{
    public class HomePage 
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;


        public HomePage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        }

        public void DriverSwitchBackToHomePage()
        {
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoBackToHomePage()
        {
            driver.Navigate().Back();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void ClickTranslinkHomePageLogo()
        {
            driver.FindElement(HomePageLocators.TranslinkHomePageLogo).Click(); 
        }
        public void ClickFaresHamMenu()
        {
            if (driver.FindElement(HomePageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HomePageLocators.HamburgerMenuButton).Click();
                driver.FindElement(HomePageLocators.FaresLink).Click();
                return;
            }

            else
            {
                driver.FindElement(HomePageLocators.FaresLink).Click();
            }
        }

        public void GoToTransitAlerts()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(HomePageLocators.TransitAlertsButton));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToFares()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(HomePageLocators.TransitFareCard));
        }

        public void GoToContactUs()
        {
            if (driver.FindElement(HomePageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HomePageLocators.HamburgerMenuButton).Click();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(HomePageLocators.ContactUsCard));
                return;
            }

            else
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(HomePageLocators.ContactUsCard));
            }
            //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript("arguments[0].click()", driver.FindElement(ContactUsCard));
        }

        public void GoToRiderInfo()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(HomePageLocators.RiderInfoCard));
        }

        public void GoToSchedules()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(HomePageLocators.SchedulesCard));
        }
    }
}
