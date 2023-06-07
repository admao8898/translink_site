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
        public readonly string nextBusURL = "https://nb.translink.ca";

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

        public static readonly By FirstSearchResult = By.XPath("(//article[@class='SearchResultItem']//a)[1]");

        public static readonly By BrowseRoutesContainer = By.XPath("(//*[text()='Browse all bus routes'])[2]");
        public static readonly By Route8 = By.XPath("//*[text()='8 Fraser / Downtown']");
        public static readonly By Route8TopDirection = By.XPath("(//*[text()='To Fraser'])[2]");
        public static readonly By Route8Stop = By.XPath("(//*[text()='E Broadway at Kingsway'])[2]");

        public static readonly By RouteR2 = By.XPath("//*[text()='R2 Marine Dr']");
        public static readonly By RouteR2TopDirection = By.XPath("(//*[text()='To Marine Dr To Phibbs Exch'])[2]");
        public static readonly By RouteR2Stop = By.XPath("(//*[text()='Marine Dr at Capilano Rd'])[2]");


        public static readonly By RouteTopDestination = By.XPath("(//*[@class='InfoCard indexLinkInfoCardTheme layoutItemContent indexLink'])[1]");
        public static readonly By RouteBottomDestination = By.XPath("(//*[@class='InfoCard indexLinkInfoCardTheme layoutItemContent indexLink'])[2]");
        public static readonly By SecondStop = By.XPath("//*/article[2]");
        public static readonly By TryNewNBLink = By.LinkText("Try the new Next Bus");

        public readonly string nextBusPageTitle = "Try out the new Next Bus for a quick way to look up departure, real time, " +
            "or scheduled times for specific stops, routes, stations, and lines";
        public readonly string nextBusPageTitleFailMsg = "Next bus Page Title Missing";
    }
}
