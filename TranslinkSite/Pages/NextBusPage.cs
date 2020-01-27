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
    public class NextBusPageElements
    {
        //Next Bus ~ "NB"
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By NextBusTab = By.Id("next-bus");
        private static readonly By NextBusField = By.Name("nextBusQuery");
        private static readonly By FindNB_Button = By.Id("carouselNextBus");
        private static readonly By TryNewNBLink = By.LinkText("Try the new Next Bus");

        public NextBusPageElements(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void TextViewNB (string busroute)
        {
            driver.FindElement(NextBusTab).Click();
            driver.FindElement(NextBusField).SendKeys(busroute);
            driver.FindElement(FindNB_Button).Click();

            Assert.IsTrue(driver.Url.Contains(busroute), "Incorrect Bus Route is Displayed");

        }


    }
}
