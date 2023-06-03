using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;
using TranslinkSite.Locators;


namespace TranslinkSite.Pages
{
    public class FaresPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

 

        public FaresPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public void BackToFaresPage()
        {
            driver.Navigate().Back(); 
        }

        public void ClickFaresLink()
        {
            if(driver.FindElement(FaresPageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(FaresPageLocators.HamburgerMenuButton).Click();
                driver.FindElement(FaresPageLocators.FaresLink).Click();
                return; 
            }

            else
            {
                driver.FindElement(FaresPageLocators.FaresLink).Click();
            }
        }

        public void ClickPriceFareZones()
        {
           if (driver.FindElement(FaresPageLocators.HamburgerMenuButton).Displayed)
           {
                driver.FindElement(FaresPageLocators.Price_Fares_ZonesMobile).Click();
                return; 
           }

           else
           {
           }
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FaresPageLocators.Price_Fares_ZonesNonMobile));
                //driver.FindElement(Price_Fares_ZonesNonMobile).Click();
        }

        public void ClickCompassCard()
        {
            bool hamMenu = driver.FindElement(FaresPageLocators.HamburgerMenuButton).Displayed; 
            if (hamMenu == true)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FaresPageLocators.CompassCardContainerMobile));
                return;
            }

            else
            {
                driver.FindElement(FaresPageLocators.CompassCardContainerNonMobile).Click();
            }
        }

        public void GoToCompassCardCard()
        {
            //user exception of element not clickable at point (x,y), tried Actions method doesn't work. 
            //Use the below method instead with reference to 
            //https://stackoverflow.com/questions/38923356/element-is-not-clickable-at-point-other-element-would-receive-the-click
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FaresPageLocators.CompassCardButton));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Because clicking on link opens new tab, driver must switch windows
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
    }
}
