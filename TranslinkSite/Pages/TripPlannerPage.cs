using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using TranslinkSite.HelperFunctions;
using TranslinkSite.Locators;
using static TranslinkSite.HelperFunctions.DropdownListVerifier;
using static TranslinkSite.HelperFunctions.ExcelToDataTableConverter;

namespace TranslinkSite.Pages
{
    public class TripPlannerPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        // ** Time and Departing Option hidden on mobile view 

        public TripPlannerPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToTripPlannerLink()
        {
            if (driver.FindElement(TripPlanningPageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(TripPlanningPageLocators.HamburgerMenuButton).Click();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TripPlanningPageLocators.TripPlannerTextLink));
                return;
            }

            else
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TripPlanningPageLocators.TripPlannerTextLink));
            }
        }

        public void GoToTripPlannerURL()
        {
            TripPlanningPageLocators tripPlanningPageLocators = new TripPlanningPageLocators();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(tripPlanningPageLocators.tripPlannerURL);
        }

        public void EnterFromDestinationText(string startingPoint)
        {
            driver.FindElement(TripPlanningPageLocators.FromTextBox).SendKeys(startingPoint); 
        }

        public void EnterToDestinationText(string endPoint)
        {
            driver.FindElement(TripPlanningPageLocators.ToTextBox).SendKeys(endPoint);
        }

        public void ClickPlanMyTripButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TripPlanningPageLocators.PlanMyTripButton));
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            Thread.Sleep(5000); //Allow time for Google Map to load
        }

        public void EscKey()
        {
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Escape);
        }

        public void GoToTripPlanningTranslink()
        {
            TripPlanningPageLocators tripPlanningPageLocators = new TripPlanningPageLocators();
            driver.Navigate().GoToUrl(tripPlanningPageLocators.TLtripPlanningURL);
            driver.FindElement(TripPlanningPageLocators.CloseWelcomeModalButton).Click();
        }

        public void ClickRoutesWidgetTab()
        {
            driver.FindElement(TripPlanningPageLocators.RouteWidgetTab).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        public void SelectRouteDropdownOption(string routeOption)
        {
            string finalXpath = string.Format(TripPlanningPageLocators.RouteDirectionOption, routeOption);
            Thread.Sleep(2000); //Allow time for dropdown to load  

            var element = driver.FindElement(By.XPath(finalXpath));

            // Use Actions to move to the element before clicking
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();

            element.Click();

            Thread.Sleep(2000); //Allow time for route to load
        }

        public void EnterRouteSearch(string routeOption)
        {
            driver.FindElement(TripPlanningPageLocators.RouteSearchInputField).Click();
            driver.FindElement(TripPlanningPageLocators.RouteSearchInputField).Clear();
            driver.FindElement(TripPlanningPageLocators.RouteSearchInputField).SendKeys(routeOption + Keys.Enter);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
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
    }
}
