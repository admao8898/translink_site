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
        private static readonly By NextBusField = By.Name("NextBusSearchTerm");
        private static readonly By NextBusTextField = By.Id("MainContent_textStop");
        private static readonly By FindNB_Button = By.XPath("//button[contains(text(),'Find my next bus')]");
        private static readonly By SubmitNextBusButton = By.Id("MainContent_linkSearch");
        private static readonly By SettingsTab = By.Id("myPreferenceUrl");
        private static readonly By ClockTime = By.XPath("//*[@value='clockTime']");
        private static readonly By CountDown = By.XPath("//*[@value='countDown']");
        private static readonly By TextView = By.XPath("//*[@value='text']");
        private static readonly By MapView = By.XPath("//*[@value='map']");

        private static readonly By MapViewTab = By.Id("mapview_tab");
        private static readonly By RefreshPage = By.Id("refresh_tab");

        private static readonly By BrowseRoutesContainer = By.XPath("(//*[text()='Browse all bus routes'])[2]");
        private static readonly By Route8 = By.XPath("//*[text()='8 Fraser / Downtown']");
        private static readonly By Route8TopDirection = By.XPath("(//*[text()='To Fraser'])[2]");
        private static readonly By Route8Stop = By.XPath("(//*[text()='E Broadway at Kingsway'])[2]");

        private static readonly By RouteR2 = By.XPath("//*[text()='R2 Marine Dr']");
        private static readonly By RouteR2TopDirection = By.XPath("(//*[text()='To Marine Dr To Phibbs Exch'])[2]");
        private static readonly By RouteR2Stop = By.XPath("(//*[text()='Marine Dr at Capilano Rd'])[2]");


        private static readonly By RouteTopDestination = By.XPath("//*[@id='MainContent_PanelStops']/*/section[3]/article");
        private static readonly By RouteBottomDestination = By.XPath("//*[@id='MainContent_PanelStops']/*/article");
        private static readonly By SecondStop = By.XPath("//*/article[2]");
        private static readonly By TryNewNBLink = By.LinkText("Try the new Next Bus");

        public readonly string nextBusPageTitle = "Next Bus is a quick way to look up departure, real time, " +
            "or scheduled times for a specific bus stop and bus route.";
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
            driver.FindElement(NextBusField).SendKeys(busRoute);
        }

        public void ClickFindBusRoute()
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FindNB_Button));
            driver.FindElement(FindNB_Button).Click(); 
        }
               
        // Two options for Time Display 
        public void ChangeTimeDisplaySettings(string timedDisplay)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SettingsTab));

            switch (timedDisplay)
            {
                case "ClockTime":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(ClockTime));
                    break;

                case "CountDown":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(CountDown));
                    break;

                default:
                    throw new Exception("Error: Please Include Desired Setting Value of either: ClockTime or Countdown");
            }

            driver.Navigate().Back();
        }

        // Two options for View Preference 
        public void ChangeViewPreferenceSettings(string viewPreference)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SettingsTab));

            switch(viewPreference)
            {
                case "TextView":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(TextView));
                    break;

                case "MapView":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(MapView));
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be MapView or TextView", "View Preference Type");
            }

            driver.Navigate().Back();
        }

        public void ClickBusDestination(string choice)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            if (choice == "Top")
            {
                //jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteTopDestination));
                driver.FindElement(RouteTopDestination).Click();
                return;
            }

            if (choice == "Bottom")
            {
                // https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
                //WebDriverWait waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(100)); 
                //waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(RouteBottomDestination)).Click();
                IWebElement element = driver.FindElement(RouteBottomDestination);

                //new Actions(driver).MoveToElement(element).MoveByOffset(631,683).Click().Perform();
                //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + ele.getLocation().y + ")");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView()", driver.FindElement(RouteBottomDestination));

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
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView()", driver.FindElement(SecondStop));
            driver.FindElement(SecondStop).Click();
            //var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(SecondStop)); 
        }

        public void ClickMapViewOption()
        {
            driver.FindElement(MapViewTab).Click();
        }

        public void ClickHiddenRefreshButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(RefreshPage));
        }

        public void ClickBrowseAllRoutes()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(BrowseRoutesContainer));
        }

        public void ClickBusRoute(string routeName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (routeName)
            {
                case "8":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(Route8));
                    break;

                case "R2":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteR2));
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be 8 Fraser or R2", "Bus Route Choice");
            }
        }

        public void ClickBrowseBusDestination(string routeName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (routeName)
            {
                case "8":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(Route8TopDirection));
                    break;

                case "R2":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteR2TopDirection));
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be 8 Fraser or R2", "Bus Browse Destination Choice");
            }        
        }

        public void ClickBrowseBusStop(string routeName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (routeName)
            {
                case "8":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(Route8Stop));
                    break;

                case "R2":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteR2Stop));
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be 8 Fraser or R2", "Bus Browse Stop Choice");
            }
        }
    }
}
