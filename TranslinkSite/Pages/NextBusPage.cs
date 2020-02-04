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
    public class NextBusPage
    {
        //Next Bus ~ "NB"
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By NextBusTab = By.Id("next-bus");
        private static readonly By NextBusMenuLink = By.XPath("//a[.='Next Bus']");
        private static readonly By NextBusField = By.Name("nextBusQuery");
        private static readonly By FindNB_Button = By.Id("carouselNextBus");
        private static readonly By Settings = By.Id("myPreferenceUrl");
        private static readonly By ClockTime = By.Id("hhmm"); 

        private static readonly By MapView = By.Id("mapview_tab");
        private static readonly By RefreshPage = By.Id("refresh_tab"); 

        private static readonly By RouteTopDestination = By.XPath("//a[contains(@href,'direction/EAST')]");
        private static readonly By RouteBottomDestination = By.XPath("//a[contains(@href,'direction/WEST')]");
        private static readonly By R5RapidBusStop = By.XPath("//a[.='Kootenay Loop Bay 8']");
        private static readonly By TryNewNBLink = By.LinkText("Try the new Next Bus");
        
        public NextBusPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
        }

        public void TextViewNB (string busroute)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            //apply a switch case for couple of other routes 

            driver.FindElement(NextBusMenuLink).Click();
            driver.FindElement(NextBusField).SendKeys(busroute);
            //driver.FindElement(FindNB_Button).Click();
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(FindNB_Button));


            //Set Real Time Display to Clock Time instead default of Countdown 
            driver.FindElement(Settings).Click();
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Time display")), "Setting Table: Time Display is Missing");

            //driver.FindElement(ClockTime).Click();
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(ClockTime));

            driver.Navigate().Back(); 
        
            Assert.IsTrue(driver.Url.Contains(busroute), "Incorrect Bus Route is Displayed");
            driver.FindElement(RouteTopDestination).Click(); 
            //driver.FindElement(R5RapidBusStop).Click();
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(R5RapidBusStop));


            //View In Map View 
            driver.FindElement(MapView).Click();
            Thread.Sleep(2000);

            //Refresh Button used to be in list of tabs and now is hidden
            //B/c of this must use JS to click it 
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(RefreshPage));
            Thread.Sleep(2000); 
        
        }
     

    }
}
