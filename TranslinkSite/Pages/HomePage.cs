using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;

namespace TranslinkSite.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // fares dropdown options 
        private static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        private static readonly By TranslinkHomePageLogo = By.ClassName("SiteLogo"); 
        private static readonly By FaresLink = By.XPath("//*[text()='Fares']");
        private static readonly By Price_Fares_ZonesNonMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[1]");
        private static readonly By CompassCardContainerNonMobile = By.XPath("(//*[@href='/transit-fares/compass-card'])[1]");
        private static readonly By Price_Fares_ZonesMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[2]");
        private static readonly By CompassCardContainerMobile = By.XPath("(//*[@href='/transit-fares/compass-card'])[2]");

        // transit alerts
        private static readonly By TransitAlertsButton = By.LinkText("Alerts Signup");
        public readonly string TranslinkTitle = "Welcome to TransLink";
        public readonly string TranslinkDescript = "Bringing the people and places of Metro Vancouver together.";
        public readonly string TranslinkTitleErrorMsg = "Incorrect Page Title";
        public readonly string TranslinkDescriptErrorMsg = "Incorrect Page Description";

        // transit general info: Fares, Rider Info, Contact Us, Schedules 
        private static readonly By TransitFareCard = By.LinkText("Fares and Zones"); 
        private static readonly By RiderInfoCard = By.LinkText("Rider Guide");
        private static readonly By ContactUsCard = By.LinkText("Contact Us");
        private static readonly By SchedulesCard = By.LinkText("Schedules & Maps");

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
            driver.FindElement(TranslinkHomePageLogo).Click(); 
        }
        public void ClickFaresHamMenu()
        {
            if (driver.FindElement(HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HamburgerMenuButton).Click();
                driver.FindElement(FaresLink).Click();
                return;
            }

            else
            {
                driver.FindElement(FaresLink).Click();
            }
        }

        public void GoToTransitAlerts()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToFares()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TransitFareCard));
        }

        public void GoToContactUs()
        {
            if (driver.FindElement(HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HamburgerMenuButton).Click();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(ContactUsCard));
                return;
            }

            else
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(ContactUsCard));
            }
            //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript("arguments[0].click()", driver.FindElement(ContactUsCard));
        }

        public void GoToRiderInfo()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(RiderInfoCard));
        }

        public void GoToSchedules()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(SchedulesCard));
        }
    }
}
