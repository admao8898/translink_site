using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslinkSite.Locators
{
    public class NextBusPageLocators
    {
        public readonly string nextBusURL = "https://www.translink.ca/next-bus";

        public static readonly By Schedules_MapsDropdown = By.XPath("//button[contains(@aria-label, 'Subpages for Schedules and Maps page')]");
        public static readonly By BusOption = By.LinkText("Bus");
        public static readonly By FindScheduleSearchBox = By.Id("find-schedule-searchbox");
        public static readonly By FindScheduleButton = By.XPath("//span[contains(text(), 'Find Schedule')]");

        public static readonly By NextBusField = By.Name("searchQuery");
        public static readonly By FindNB_Button = By.XPath("//button[@class='flexContainer largeViewOnlyContent']" +
            "/span[contains(text(),'Find my next bus')]"); //this is for desktop view
        public static readonly By UseCurrentLocationButton = By.XPath("//transit-near-me-link//a[@class='flexContainer']");
        public static readonly By SubmitNextBusButton = By.Id("MainContent_linkSearch");
        public static readonly By SettingsTab = By.LinkText("Settings");
        public static readonly By ClockTime = By.XPath("//*[@value='clockTime']");
        public static readonly By CountDown = By.XPath("//*[@value='countDown']");
        public static readonly By MapView = By.XPath("//a[text()='View route on map' and @class ='flexContainer']"); //Toggle for text view as well
        
        public static readonly By NearbyMapView = By.XPath("//a[text()='Go to map view' and @class ='flexContainer']");

        public static readonly By RefreshPage = By.Id("refresh_tab");

        public static readonly string RouteDirectionOption = "//a[strong[contains(text(), '{0}')]]";

        public readonly string nextBusPageHeader = "Next Bus";
        public readonly string nextBusPageTitleFailMsg = "Next bus Page Title Missing";
    }
}
