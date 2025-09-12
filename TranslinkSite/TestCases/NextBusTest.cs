using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TranslinkSite.Pages;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TranslinkSite.Locators;

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
            NextBusPage nextBusPage = new NextBusPage(driver);
            NextBusPageLocators nextBusPageLocators = new NextBusPageLocators();

            nextBusPage.GoToNextBus();
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(nextBusPageLocators.nextBusPageHeader)),
                nextBusPageLocators.nextBusPageTitleFailMsg);
            nextBusPage.EnterBusRoute(busRoute);
            nextBusPage.ClickRouteDirection(routeDirection);
            nextBusPage.ClickMapView("Route"); //observe it in Mapview (note this is toggle for text view as well)
            Thread.Sleep(5000);
            Assert.IsTrue(driver.Url.Contains(busRoute), "Incorrect Bus Route is Displayed. It's not Route " + busRoute);
            nextBusPage.TakeScreenShotMapView();

        }

        [TestCase(), Category("Smoke")]
        public void CurrentLocationScreenshot()
        {
            NextBusPage nextBusPage = new NextBusPage(driver);
            nextBusPage.GoToNextBus();
            nextBusPage.ClickCurrentLocation();
            Thread.Sleep(1000);
            nextBusPage.ClickMapView("GPS"); //observe it in Mapview (note this is toggle for text view as well)
            nextBusPage.ScrollPageDirection("down");
            Thread.Sleep(3000);
            nextBusPage.TakeScreenShotMapView();
        }

        [TestCase("99", "#99 - UBC B-Line"), Category("Smoke")]
        [TestCase("19", "#19 - Stanley Park")]
        public void BusSchedulesLookUp(string route, string RouteDestination)
        {
            NextBusPage nextBusPage = new NextBusPage(driver);
            nextBusPage.ClickSchedules_MapsDropdown();
            nextBusPage.ClickBusOption(); 
            Assert.IsTrue(driver.Url.Contains("bus-schedules"), "Not on Bus Schedules Page");
            nextBusPage.EnterBusRoute(route);
            nextBusPage.ClickRouteDirection(RouteDestination);



        }
    }
}

