using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslinkSite.Locators
{
    public class FaresPageLocators
    {
        public static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        public static readonly By FaresLink = By.XPath("//*[text()='Fares']");
        public static readonly By Price_Fares_ZonesNonMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[1]");
        public static readonly By CompassCardContainerNonMobile = By.XPath("(//*[@href='/transit-fares/compass-card'])[1]");
        public static readonly By Price_Fares_ZonesMobile = By.XPath("(//*[@href='/transit-fares/pricing-and-fare-zones'])[2]");
        public static readonly By CompassCardContainerMobile = By.XPath("//*[text()='Learn more about fares and transit passes, concession fares, and available payment methods. ']");

        // compass card 
        public static readonly By CompassCardButton = By.XPath("//a[.='Visit compasscard.ca']");
        public readonly string CompassCardTitle = "Compass is your key!";
        public readonly string CompassCardTitleFailMsg = "Compass Article Title is Incorrect";
        public readonly string CompassCardDescription = "TransLink's reloadable fare card that works everywhere on transit.";
        public readonly string CompassCardDescriptionFailMsg = "Compass Article Body is Incorrect";
    }
}
