using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TranslinkSite.Pages;
using SeleniumExtras.WaitHelpers;
using TranslinkSite.Locators;

namespace TranslinkSite.TestCases
{
    public class NextBusTest : UITestFixture
    {
        //string busrouteST; 
        //Next Bus ~ "NB"
        //Goes directly to next bus link. Does not use next bus feature on homepage 
        [TestCase("99"), Category("Smoke")]
        [TestCase("19")]
        [TestCase("319")]
        public void NextBusRouteInput(string busRoute)
        {
            NextBusPage nextBusPage = new NextBusPage(driver);
            NextBusPageLocators nextBusPageLocators = new NextBusPageLocators();

            nextBusPage.GoToNextBus();
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(nextBusPageLocators.nextBusPageTitle)),
                nextBusPageLocators.nextBusPageTitleFailMsg);
            //string busRouteST = busRoute.ToString(); 
            nextBusPage.EnterBusRoute(busRoute);
            nextBusPage.PressEnterKey();
            nextBusPage.ClickFirstSearchResult();   
            nextBusPage.ClickMapView("Route"); //observe it in Mapview (note this is toggle for text view as well)
            Thread.Sleep(3000);
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
            Thread.Sleep(3000);
            nextBusPage.TakeScreenShotMapView(); 
        }

        //[TestCase("R2")]
        //[TestCase("8")]
        //public void NextBusBrowseDesiredRoute(string busRoute)
        //{
        //    NextBusPage nextBusPage = new NextBusPage(driver);
        //    nextBusPage.GoToNextBus();

        //    nextBusPage.ClickBrowseAllRoutes();
        //    nextBusPage.ClickBusRoute(busRoute);
        //    nextBusPage.ClickBrowseBusDestination(busRoute);
        //    nextBusPage.ClickBrowseBusStop(busRoute);
        //    //nextBusPage.PressEnterKey();

        //    nextBusPage.ChangeTimeDisplaySettings("ClockTime");
        //    nextBusPage.ChangeViewPreferenceSettings("MapView");
        //    Assert.IsTrue(driver.Url.Contains(busRoute), "Incorrect Bus Route is Displayed");
        //    //Thread.Sleep(500);
        //    nextBusPage.ClickMapViewOption();
        //    //Thread.Sleep(500);
        //}
    }
}
