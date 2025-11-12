using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using TranslinkSite.HelperFunctions;
using TranslinkSite.Locators;


namespace TranslinkSite.Pages
{
    public class NextBusPage
    {
        //Next Bus ~ "NB"
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        
        public NextBusPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void GoToNextBus()
        {
            NextBusPageLocators nextBusPageLocators = new NextBusPageLocators();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(nextBusPageLocators.nextBusURL);
        }

        public void EnterBusRoute(string busRoute)
        {
            if(driver.Url.Contains("bus-schedules"))
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(NextBusPageLocators.FindScheduleSearchBox));
                driver.FindElement(NextBusPageLocators.FindScheduleSearchBox).SendKeys(busRoute);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(NextBusPageLocators.FindScheduleButton));
                driver.FindElement(By.TagName("body")).Click(); //click outside to close any pop-up if it appears

                //driver.FindElement(NextBusPageLocators.FindScheduleButton).Click();
                return;
            }

            else
            {
                driver.FindElement(NextBusPageLocators.NextBusField).SendKeys(busRoute + Keys.Enter);
                driver.FindElement(NextBusPageLocators.NextBusField).SendKeys(Keys.Enter);
            }
                
        }

        //Using Page Scroll helper functions 
        //Jan 27, 2024
        public void ScrollPageDirection(string direction)
        {
            PageScroller scroller = new();
            scroller.ScrollPageBy(driver,direction);
        }

        public void ClickCurrentLocation()
        {
            driver.FindElement(NextBusPageLocators.UseCurrentLocationButton).Click(); 
        }
        public void ClickFindBusRoute()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(NextBusPageLocators.FindNB_Button));
            //driver.FindElement(FindNB_Button).Click(); 
        }

        public void TakeScreenShotMapView()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                TakeScreenShot takeScreenShot = new TakeScreenShot();
                takeScreenShot.GetRegularScreenshot(driver);
            }

            else
            {
            }
        }
      
        public void ClickMapView(string ViewType)
        {
           switch (ViewType)
            {
                case "GPS":
                    driver.FindElement(NextBusPageLocators.NearbyMapView).Click();
                    break;
                case "Route":
                    driver.FindElement(NextBusPageLocators.MapView).Click();
                    break;
                default:
                    break; 
            };

         }

        public void ClickRouteDirection(string routeDirection)
        {
            string finalXpath = string.Format(NextBusPageLocators.RouteDirectionOption, routeDirection);
            driver.FindElement(By.XPath(finalXpath)).Click();
        }

        public void ClickSchedules_MapsDropdown()
        {
            if (driver.FindElement(HomePageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HomePageLocators.HamburgerMenuButton).Click();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(NextBusPageLocators.Schedules_MapsDropdown));
                return;
            }

            else
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(NextBusPageLocators.Schedules_MapsDropdown));
            }
        }
        public void ClickBusOption()
        {
            driver.FindElement(NextBusPageLocators.BusOption).Click();
        }
    }

   
}
