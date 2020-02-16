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
        private readonly string nextBusURL = "https://nb.translink.ca";

        private static readonly By NextBusTab = By.Id("next-bus");
        private static readonly By NextBusMenuLink = By.XPath("//a[.='Next Bus']");
        private static readonly By NextBusField = By.Name("nextBusQuery");
        private static readonly By NextBusTextField = By.Id("MainContent_textStop");
        private static readonly By FindNB_Button = By.Id("carouselNextBus");
        private static readonly By SubmitNextBusButton = By.Id("MainContent_linkSearch");
        private static readonly By Settings = By.Id("myPreferenceUrl");
        private static readonly By ClockTime = By.Id("hhmm");
        private static readonly By CountDown = By.Id("countdown");

        private static readonly By MapView = By.Id("mapview_tab");
        private static readonly By RefreshPage = By.Id("refresh_tab");

        private static readonly By RouteTopDestination = By.XPath("//*[@id='MainContent_PanelStops']/*/section[3]/article");
        private static readonly By RouteBottomDestination = By.XPath("//*[@id='MainContent_PanelStops']/*/article");
        private static readonly By SecondStop = By.XPath("//*/article[2]");
        private static readonly By TryNewNBLink = By.LinkText("Try the new Next Bus");

        public readonly string nextBusPageTitle = "Next bus departures in real-time";
        public readonly string nextBusPageTitleFailMsg = "Next bus Page Title Missing";


        public NextBusPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void GoToNextBus()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(nextBusURL);
        }

        public void EnterBusRoute(string busRoute)
        {
            driver.FindElement(NextBusTextField).SendKeys(busRoute);
        }

        public void ClickFindBusRoute()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(SubmitNextBusButton));
        }

        // Two options for Time Display 
        public void ChangeSettings(string timedDisplay)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.FindElement(Settings).Click();

            if (timedDisplay == "ClockTime")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(ClockTime));
                driver.Navigate().Back();
                return;
            }

            if (timedDisplay == "CountDown")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(CountDown));
                driver.Navigate().Back();
                return;
            }

            else
            {
                throw new Exception("Error: Please Include Desired Setting Value of either: ClockTime or Countdown");
            }
        }
        public void Destination(string choice)
        {
            if (choice == "Top")
            {
                //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                //jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteTopDestination));
                driver.FindElement(RouteTopDestination).Click();
                return; 
            }

            if (choice == "Bottom")
            {
                //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                //jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteBottomDestination));
                driver.FindElement(RouteBottomDestination).Click();
                return; 
            }

            else
            {
                throw new Exception("Error: Please Include Desired Destination Value of either: Top or Bottom");
            }
        }

        public void Click2ndBusStop()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            IWebElement Stop2nd = driver.FindElement(SecondStop);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView()", Stop2nd);
            driver.FindElement(SecondStop).Click(); 
            //var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(SecondStop)); 
        }

        public void MapViewOption()
        {
            driver.FindElement(MapView).Click(); 
        }

        public void ClickHiddenRefreshButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(RefreshPage));
        } 
    }
}
