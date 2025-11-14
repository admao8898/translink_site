using DocumentFormat.OpenXml.Vml;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using TranslinkSite.Locators;
using TranslinkSite.Pages;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TranslinkSite.TestCases
{
    public class TripPlannerTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void TripPlannerLink()
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            TripPlanningPageLocators tripPlanningPageLocators = new TripPlanningPageLocators();
            tripPlannerPage.GoToTripPlannerLink();
            Assert.Contains("translink.ca/trip-planner", driver.Url, "This is not the Trip Planner page");

            //Verify Page Descriptions 
            Assert.Contains(tripPlanningPageLocators.tripPlannerPageDescription,
                driver.FindElement(By.TagName("body")).Text, tripPlanningPageLocators.tripPlannerPageDescriptFailMsg);
        }

        [TestCase(), Order(2)]
        public void TripPlannerURL()
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            TripPlanningPageLocators tripPlanningPageLocators = new TripPlanningPageLocators(); 
            tripPlannerPage.GoToTripPlannerURL();
            Assert.Contains("translink.ca/trip-planner", driver.Url, "This is not the Trip Planner page");

            //Verify Page Descriptions 
            Assert.Contains(tripPlanningPageLocators.tripPlannerPageDescription,
            driver.FindElement(By.TagName("body")).Text, tripPlanningPageLocators.tripPlannerPageDescriptFailMsg);
        }

        [TestCase("SFU", "UBC"), Order(3)]
        public void TripPlannerGMapVerify(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.ClickPlanMyTripButton();
            Assert.Contains(driver.Url.Contains("google.com/maps"), "Not Google Maps");
            Assert.Contains(driver.Url.Contains(endPoint), "Incorrect Ending Point");
            Assert.Contains(driver.Url.Contains(startPoint), "Incorrect Starting Point");
        }

        [TestCase("22nd Street Station", "VCC-Clark"), Order(4), Category("Smoke")]
        [TestCase("King Edward Station", "Surrey Central Station")]
        public void TripPlannerSampleTrip(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL(); 
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.EscKey();
            tripPlannerPage.ClickPlanMyTripButton();
        }

        [TestCase("FRASER/​WATERFRONT STN WEST"), Order(5)]
        [TestCase("CANADA LINE SKYTRAIN SOUTH")]          
        public void TripPlanTranslinkRouteDropdownSelect(string routeDestination)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);  
            tripPlannerPage.GoToTripPlanningTranslink();    
            tripPlannerPage.ClickRoutesWidgetTab(); 
            tripPlannerPage.SelectRouteDropdownOption(routeDestination);
            tripPlannerPage.TakeScreenShotMapView();
        }

        [TestCase("99", "COMMERCIAL-BROADWAY/​UBC (B-LINE) WEST"), Order(6)]
        [TestCase("351", "WHITE ROCK CTR/​BRIDGEPORT STN SOUTH")]
        [TestCase("19", "METROTOWN STN/​STANLEY PARK WEST")]
        public void TripPlanTranslinkRouteSearch(string routeNumber, string routeDestination)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlanningTranslink();
            tripPlannerPage.ClickRoutesWidgetTab();
            tripPlannerPage.EnterRouteSearch(routeNumber);
            tripPlannerPage.SelectRouteDropdownOption(routeDestination);
            tripPlannerPage.TakeScreenShotMapView();
        }
    }   
}
