using DocumentFormat.OpenXml.Vml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Locators;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class TripPlannerTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void TripPlannerLink()
        {
            TripPlannerPage tripPlannerPage = new(driver);
            TripPlanningPageLocators tripPlanningPageLocators = new TripPlanningPageLocators();

            tripPlannerPage.GoToTripPlannerLink();

            // URL verification
            StringAssert.Contains(driver.Url,
                                  "translink.ca/trip-planner",
                                  "This is not the Trip Planner page");

            // Page description verification
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  tripPlanningPageLocators.tripPlannerPageDescription,
                                  tripPlanningPageLocators.tripPlannerPageDescriptFailMsg);
        }

        [TestCase(), Order(2)]
        public void TripPlannerURL()
        {
            TripPlannerPage tripPlannerPage = new(driver);
            TripPlanningPageLocators tripPlanningPageLocators = new TripPlanningPageLocators();

            tripPlannerPage.GoToTripPlannerURL();

            // URL verification
            StringAssert.Contains(driver.Url,
                                  "translink.ca/trip-planner",
                                  "This is not the Trip Planner page");

            // Page description verification
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  tripPlanningPageLocators.tripPlannerPageDescription,
                                  tripPlanningPageLocators.tripPlannerPageDescriptFailMsg);
        }

        [TestCase("SFU", "UBC"), Order(3)]
        public void TripPlannerGMapVerify(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new(driver);

            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);
            tripPlannerPage.EnterToDestinationText(endPoint);
            tripPlannerPage.ClickPlanMyTripButton();

            // URL check: must contain Google Maps
            StringAssert.Contains(driver.Url, "google.com/maps", "Not Google Maps");

            // Map the known inputs to the actual Google Maps URL encoding
            string startPointEncoded = "Simon+Fraser+University";           // SFU
            string endPointEncoded = "University+of+British+Columbia";      // UBC

            // Assert that the URL contains the encoded start and end points
            StringAssert.Contains(driver.Url, startPointEncoded, "Incorrect Starting Point");
            StringAssert.Contains(driver.Url, endPointEncoded, "Incorrect Ending Point");
        }

        [TestCase("22nd Street Station", "VCC-Clark"), Order(4), Category("Smoke")]
        [TestCase("King Edward Station", "Surrey Central Station")]
        public void TripPlannerSampleTrip(string startPoint, string endPoint)
        {
            TripPlannerPage tripPlannerPage = new(driver);

            tripPlannerPage.GoToTripPlannerURL();
            tripPlannerPage.EnterFromDestinationText(startPoint);

        }

        [TestCase("FRASER/​WATERFRONT STN WEST"), Order(5)]
        [TestCase("MILLENNIUM SKYTRAIN EAST")]
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
