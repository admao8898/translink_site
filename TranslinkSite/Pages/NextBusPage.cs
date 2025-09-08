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
            driver.FindElement(NextBusPageLocators.NextBusField).SendKeys(busRoute + Keys.Enter);
            driver.FindElement(NextBusPageLocators.NextBusField).SendKeys(Keys.Enter);

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
      
        //// Two options for Time Display 
        //public void ChangeTimeDisplaySettings(string timedDisplay)
        //{
        //    IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
        //    jse.ExecuteScript("arguments[0].click()", driver.FindElement(SettingsTab));

        //    switch (timedDisplay)
        //    {
        //        case "ClockTime":
        //            jse.ExecuteScript("arguments[0].click()", driver.FindElement(ClockTime));
        //            break;

        //        case "CountDown":
        //            jse.ExecuteScript("arguments[0].click()", driver.FindElement(CountDown));
        //            break;

        //        default:
        //            throw new Exception("Error: Please Include Desired Setting Value of either: ClockTime or Countdown");
        //    }

        //    driver.Navigate().Back();
        //}

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

        //// Two options for View Preference 
        //public void ChangeViewPreferenceSettings(string viewPreference)
        //{
        //    IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
        //    jse.ExecuteScript("arguments[0].click()", driver.FindElement(SettingsTab));

        //    switch(viewPreference)
        //    {
        //        case "TextView":
        //            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TextView));
        //            break;

        //        case "MapView":
        //            jse.ExecuteScript("arguments[0].click()", driver.FindElement(MapView));
        //            break;

        //        default:
        //            throw new System.ArgumentException("Parameter must either be MapView or TextView", "View Preference Type");
        //    }

        //    driver.Navigate().Back();
        //}

        public void ClickBusDestination(string choice)
        {

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;


            if (choice == "Top")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(NextBusPageLocators.RouteTopDestination));
                //driver.FindElement(RouteTopDestination).Click();
                return;
            }

            if (choice == "Bottom")
            {
                // https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
                //WebDriverWait waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(100)); 
                //waiter.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(RouteBottomDestination)).Click();
                IWebElement element = driver.FindElement(NextBusPageLocators.RouteBottomDestination);

                //new Actions(driver).MoveToElement(element).MoveByOffset(631,683).Click().Perform();
                //((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + ele.getLocation().y + ")");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView()", driver.FindElement(NextBusPageLocators.RouteBottomDestination));

                //jse.ExecuteScript("arguments[0].click()", driver.FindElement(RouteBottomDestination));

                driver.FindElement(NextBusPageLocators.RouteBottomDestination).Click();
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
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView()", driver.FindElement(NextBusPageLocators.SecondStop));
            driver.FindElement(NextBusPageLocators.SecondStop).Click();
            //var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(SecondStop)); 
        }

        public void ClickHiddenRefreshButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(NextBusPageLocators.RefreshPage));
        }

        public void ClickRouteDirection(string routeDirection)
        {
            string finalXpath = string.Format(NextBusPageLocators.RouteDirectionOption, routeDirection);
            driver.FindElement(By.XPath(finalXpath)).Click();

        }
    }

   
}
