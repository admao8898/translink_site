using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TranslinkSite.Locators
{
    public class HomePageLocators
    {

        // fares dropdown options 
        public static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        public static readonly By TranslinkHomePageLogo = By.ClassName("SiteLogo");
        public static readonly By FaresLink = By.XPath("//*[text()='Fares']");
        public static readonly By Price_Fares_ZonesNonMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[1]");
        public static readonly By CompassCardContainerNonMobile = By.XPath("(//*[@href='/transit-fares/compass-card'])[1]");
        public static readonly By Price_Fares_ZonesMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[2]");
        public static readonly By CompassCardContainerMobile = By.XPath("(//*[@href='/transit-fares/compass-card'])[2]");

        // transit alerts
        public static readonly By TransitAlertsLink = By.LinkText("Alerts");
        public readonly string TranslinkTitle = "Welcome to TransLink";
        public readonly string TranslinkDescript = "Bringing the people and places of Metro Vancouver together.";
        public readonly string TranslinkTitleErrorMsg = "Incorrect Page Title";
        public readonly string TranslinkDescriptErrorMsg = "Incorrect Page Description";

        // transit general info: Fares, Rider Info, Contact Us, Schedules 
        public static readonly By TransitFareCard = By.LinkText("Fares and Zones");
        public static readonly By RiderInfoCard = By.LinkText("Rider Guide");
        public static readonly By ContactUsCard = By.LinkText("Contact");
        public static readonly By SchedulesCard = By.LinkText("Schedules & Maps");
    }
}
