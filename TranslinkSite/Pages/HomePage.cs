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
        private static readonly By FaresLink = By.XPath("//*[text()='Fares']");
        private static readonly By FaresDropDownLink = By.XPath("//*[@aria-label = 'Subpages for Fares page']");
        private static readonly By CompassCardDDLink = By.XPath("//*[text()='Compass Card']"); 

        // transit alerts
        private static readonly By TransitAlertsButton = By.LinkText("Sign up to receive transit alerts");
        public readonly string TransitAlertsCardTitle = "Know before you go!";
        public readonly string TransitAlertsCardTitleFailMsg = "Alerts Article Title is Incorrect";
        public readonly string TransitAlertsCardDescription = "Create notifications for the transit services that matter most to you. " +
            "Sign up to receive transit alerts via SMS or email.";
        public readonly string TransitAlertsCardDescriptionFailMsg = "Alerts Article Body is Incorrect";

        // transit general info: Fares, Rider Info, Contact Us, Schedules 
        private static readonly By TransitFareCard = By.LinkText("Transit Fares"); 
        private static readonly By RiderInfoCard = By.LinkText("Rider Info");
        private static readonly By ContactUsCard = By.LinkText("Contact Us");
        private static readonly By SchedulesCard = By.LinkText("Schedules");

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

        public void ClickFaresLink()
        {
            driver.FindElement(FaresLink).Click();
            driver.Navigate().Back(); 
        }

        public void ClickFaresDropdown()
        {
            driver.FindElement(FaresDropDownLink).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void ClickCompassCardSubLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(CompassCardDDLink));
            driver.Navigate().Back();
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
            //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript("arguments[0].click()", driver.FindElement(ContactUsCard));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(ContactUsCard));
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
