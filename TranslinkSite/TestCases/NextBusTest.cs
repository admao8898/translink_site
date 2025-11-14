using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TranslinkSite.Pages;
using TranslinkSite.Locators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranslinkSite.TestCases
{
    public class NextBusTest : UITestFixture
    {
        //string busrouteST; 
        //Next Bus ~ "NB"
        //Goes directly to next bus link. Does not use next bus feature on homepage 
        [TestCase("r6", "Scott Road Station"), Category("Smoke")]
        [TestCase("99", "UBC")]
        [TestCase("351", "Bridgeport")]
        public void NextBusRouteInput(string busRoute, string routeDirection)
        {
            NextBusPage nextBusPage = new(driver);
            NextBusPageLocators nextBusPageLocators = new();

            nextBusPage.GoToNextBus();

            // Verify page header
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  nextBusPageLocators.nextBusPageHeader,
                                  nextBusPageLocators.nextBusPageTitleFailMsg);

            nextBusPage.EnterBusRoute(busRoute);

            // Optional: uncomment and fix when ready
            // nextBusPage.ClickRouteDirection(routeDirection);
            // nextBusPage.ClickMapView("Route"); // observe in Map view
            // Thread.Sleep(5000);
            // StringAssert.Contains(driver.Url, busRoute, $"Incorrect Bus Route displayed. It's not Route {busRoute}");
            // nextBusPage.TakeScreenShotMapView();
        }

        [TestCase(), Category("Smoke")]
        public void CurrentLocationScreenshot()
        {
            NextBusPage nextBusPage = new(driver);

            nextBusPage.GoToNextBus();
            nextBusPage.ClickCurrentLocation();
            Thread.Sleep(1000);

            nextBusPage.ClickMapView("GPS"); // observe in Map view
            nextBusPage.ScrollPageDirection("down");
            Thread.Sleep(3000);

            nextBusPage.TakeScreenShotMapView();
        }

        [TestCase("99", "#99 - UBC B-Line"), Category("Smoke")]
        [TestCase("19", "#19 - Stanley Park")]
        public void BusSchedulesLookUp(string route, string routeDestination)
        {
            NextBusPage nextBusPage = new(driver);

            nextBusPage.ClickSchedules_MapsDropdown();
            nextBusPage.ClickBusOption();

            // URL verification
            StringAssert.Contains(driver.Url,
                                  "bus-schedules",
                                  "Not on Bus Schedules Page");

            nextBusPage.EnterBusRoute(route);

            // Optional: uncomment and fix when ready
            // nextBusPage.ClickRouteDirection(routeDestination);
        }

    }
}

