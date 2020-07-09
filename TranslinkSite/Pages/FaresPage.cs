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
    public class FaresPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton"); 
        private static readonly By FaresLink = By.XPath("//*[text()='Fares']");
        private static readonly By Price_Fares_ZonesNonMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[1]");
        private static readonly By CompassCardContainerNonMobile = By.XPath("(//*[@href='/transit-fares/compass-card'])[1]");
        private static readonly By Price_Fares_ZonesMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[2]");
        private static readonly By CompassCardContainerMobile = By.XPath("//*[text()='Learn more about fares and transit passes, concession fares, and available payment methods. ']");

        // compass card 
        private static readonly By CompassCardButton = By.XPath("//a[.='Visit compasscard.ca']");
        public readonly string CompassCardTitle = "Compass is your key!";
        public readonly string CompassCardTitleFailMsg = "Compass Article Title is Incorrect";
        public readonly string CompassCardDescription = "TransLink's reloadable fare card that works everywhere on transit.";
        public readonly string CompassCardDescriptionFailMsg = "Compass Article Body is Incorrect";

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
            if(driver.FindElement(HamburgerMenuButton).Displayed)
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

        public void ClickPriceFareZones()
        {
           if (driver.FindElement(HamburgerMenuButton).Displayed)
           {
                driver.FindElement(Price_Fares_ZonesMobile).Click();
                return; 
           }

           else
           {
                driver.FindElement(Price_Fares_ZonesNonMobile).Click();
           }
        }

        public void ClickCompassCard()
        {
            bool hamMenu = driver.FindElement(HamburgerMenuButton).Displayed; 
            if (hamMenu == true)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(CompassCardContainerMobile));
                return;
            }

            else
            {
                driver.FindElement(CompassCardContainerNonMobile).Click();
            }
        }

        public void GoToCompassCardCard()
        {
            //user exception of element not clickable at point (x,y), tried Actions method doesn't work. 
            //Use the below method instead with reference to 
            //https://stackoverflow.com/questions/38923356/element-is-not-clickable-at-point-other-element-would-receive-the-click
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(CompassCardButton));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            //Because clicking on link opens new tab, driver must switch windows
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }
    }
}
