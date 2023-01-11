using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class TripPlannerTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void TripPlannerLink()
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerLink();
            Assert.IsTrue(driver.Url.Contains("translink.ca/trip-planner"), "This is not the Trip Planner page");

            //Verify Page Descriptions 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(tripPlannerPage.tripPlannerPageDescription), 
                tripPlannerPage.tripPlannerPageDescriptFailMsg);
        }

        [TestCase(), Order(2)]
        public void TripPlannerURL()
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            Assert.IsTrue(driver.Url.Contains("translink.ca/trip-planner"), "This is not the Trip Planner page");

            //Verify Page Descriptions 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(tripPlannerPage.tripPlannerPageDescription), 
                tripPlannerPage.tripPlannerPageDescriptFailMsg);
        }

        [TestCase("BCIT", "UBC"), Order(3)]
        public void TripPlannerGMapVerify(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.ClickPlanMyTripButton();
            Assert.IsTrue(driver.Url.Contains("google.com/maps"), "Not Google Maps");
            Assert.IsTrue(driver.Url.Contains(endPoint), "Incorrect Ending Point");
            Assert.IsTrue(driver.Url.Contains(startPoint), "Incorrect Starting Point");
        }

        [TestCase("Prefer"), Order(4)]
        [TestCase("Routes")]
        public void TripPlannerVerifyDropdownOptions(string type)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.VerifyAllDropdownOptions(type);
        }

        [TestCase("22nd Street Station", "VCC-Clark", "Bus", "Less walking"), Order(5), Category("Smoke")]
        public void TripPlannerSampleTrip(string startPoint, string endPoint, string preferedMode, string routeOption)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL(); 
            //tripPlannerPage.ClickMoreOptionsLink();
            //tripPlannerPage.SelectPreferedTransitMode(preferedMode);
            //tripPlannerPage.SelectPreferedRouteMode(routeOption);
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.EscKey();
            tripPlannerPage.ClickPlanMyTripButton();
        }

        [TestCase("King Edward Station", "Surrey Central Station"), Order(6), Category("Smoke")]
        public void TripPlannerSwitchDirections(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            //Thread.Sleep(2000);
            tripPlannerPage.ClickChangeDirectionButton();
            //Thread.Sleep(2000);
            tripPlannerPage.ClickPlanMyTripButton();
        }
    }   
}
