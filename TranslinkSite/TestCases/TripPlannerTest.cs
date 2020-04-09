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
        [TestCase(), Order(1), Category("Smoke")]
        public void TripPlannerLink()
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerLink();
            Assert.IsTrue(driver.Url.Contains("translink.ca/trip-planner"), "This is not the Trip Planner page");

            //Verify Page Descriptions 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(tripPlannerPage.tripPlannerPageDescription), tripPlannerPage.tripPlannerPageDescriptFailMsg);
        }

        [TestCase(), Order(2), Category("Smoke")]
        public void TripPlannerURL()
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();          
            Assert.IsTrue(driver.Url.Contains("translink.ca/trip-planner"), "This is not the Trip Planner page");

            //Verify Page Descriptions 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(tripPlannerPage.tripPlannerPageDescription), tripPlannerPage.tripPlannerPageDescriptFailMsg);
        }

        [TestCase("SFU", "UBC"), Order(3)]
        public void TripPlannerGMapVerify(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.ClickPlanMyTripButton();
            tripPlannerPage.VerifyGoogleMaps(); 
        }

        [TestCase("Prefer"), Order(4)]
        [TestCase("Routes")]
        public void TripPlannerVerifyDropdownOptions(string type)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.VerifyAllDropdownOptions(type); 
        }

        [TestCase("New Westminster Station", "UBC", "Bus", "Less walking"),Order(5)]
        public void SampleTrip(string startPoint, string endPoint, string preferedMode, string routeOption)
        {
            TripPlannerPage tripPlannerPage = new TripPlannerPage(driver);
            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.ClickMoreOptionsLink(); 
            tripPlannerPage.SelectPreferedTransitMode(preferedMode);
            tripPlannerPage.SelectPreferedRouteMode(routeOption); 
            tripPlannerPage.ClickPlanMyTripButton();
        }
    }
}
