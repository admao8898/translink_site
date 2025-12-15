using OpenQA.Selenium;

namespace TranslinkSite.Locators
{
    public class TripPlanningPageLocators
    {
        public readonly string tripPlannerURL = "https://translink.ca/trip-planner";
        public readonly string TLtripPlanningURL = "https://tripplanning.translink.ca/";

        public static readonly By CloseWelcomeModalButton = By.XPath("//button[contains(text(), 'Close')]");
        public static readonly By RouteWidgetTab = By.XPath("//div[@class='routeswidget-tab']");
        public static By RouteOption(string text) => By.XPath($".//div[contains(text(), '{text}')]");
        public static readonly By RouteSearchInputField = By.XPath("//input[@placeholder='Search Routes']");
        public static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");

        public static readonly By TripPlannerTextLink = By.XPath("//*[text()='Trip Planner']");

        public readonly string tripPlannerPageDescription = "Tell us where you're starting from and where you want to go and we'll find the " +
            "best route to get you there.";
        public readonly string tripPlannerPageDescriptFailMsg = "Incorrect Trip Planner Description";

        //Google Map Text 
        public static readonly By GMapStartPoint = By.XPath("//*[@aria-label='Starting point Simon Fraser University']");
        public static readonly By GMapEndPoint = By.XPath("//*[@aria-label='Destination The University of British Columbia']");

        public static readonly By FromTextBox = By.Id("prev_point_desktop");
        public static readonly By ToTextBox = By.Id("next_point_desktop");
        public static readonly By PlanMyTripButton = By.XPath("//*[text()='Plan my trip']");

    }
}