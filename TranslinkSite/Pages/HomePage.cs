using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.TestCases;

namespace TranslinkSite.Pages
{
    public class HomePageElements
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        //compass card elements
        private static readonly By CompassCardButton = By.LinkText("Visit compasscard.ca");

        public HomePageElements(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); 
        }

        public void CompassCard()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            IWebElement CompassCardLink = driver.FindElement(HomePageElements.CompassCardButton);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", CompassCardLink);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            
            //Because clicking on link opens new tab, driver must switch windows
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Assert.IsTrue(driver.Url.Contains("compasscard")); driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            driver.SwitchTo().Window(driver.WindowHandles.First());
            
        }
    }
}
