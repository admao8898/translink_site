using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslinkSite.Locators
{
    public class FeedbackPageLocators
    {
        public readonly string feedbackURL = "https://translink.ca/feedback";
        public readonly string namePipelineVariable = Environment.GetEnvironmentVariable("name", EnvironmentVariableTarget.Process);

        //Link From homepage 
        public static readonly By CustomerFeedbackLink = By.XPath("//a[.='Customer Feedback']");
        public static readonly By ShareYourThoughtsLink = By.LinkText("Share your thoughts");
        public static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        public static readonly By ContactUs = By.LinkText("Contact");


        public readonly string feedbackDescription = "We're here to help! Use this form to send us questions, " +
            "lost item inquiries, comments and suggestions.";
        public readonly string feedbackDescriptFailMsg = "Incorrect Feedback Description";
        public readonly string dropDownTitle = "What is your feedback regarding";
        public readonly string dropDownTitleFailMsg = "Incorrect Dropdown Title Displayed";

        public static readonly By FBTypeDropdownSelector = By.Name("FeedbackTopic");

        //XPath query to get nth instance of an element 
        // https://stackoverflow.com/questions/4007413/xpath-query-to-get-nth-instance-of-an-element
        public static readonly By BusFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[2]");
        public static readonly By SkyTrainFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[3]");

        public static readonly By CustomerFeedbackTitle = By.XPath("//h2[text()='Customer Feedback']");
        public static readonly By CustomerFeedbackDescription = By.XPath("");

        //Bus Feedback Form 
        public readonly string routeNumberLegend = "Route Number";
        public readonly string routeNumberLegendFailMsg = "Route Number Legend Missing";
        public static readonly By RouteNumberField = By.Id("busfeedback-routenumber");
        public static readonly By BusIncidentDateField = By.Id("busfeedback-incidentdatetime");
        public static readonly By BusIncidentTimeField = By.Id("busfeedback-incidentdatetime");
        public static readonly By BusPhoneNumberField = By.Id("busfeedback-phonenumber");
        public static readonly By BusFirstNameField = By.Id("busfeedback-firstname");
        public static readonly By BusCustRepResponseYesButton = By.XPath("(//*[.='Yes'])[3]");
        public static readonly By BusCustRepResponseNoButton = By.XPath("(//*[.='No'])[3]");

        //Skytrain FeedBack Form 
        public readonly string skytrainLineLegend = "SkyTrain Line";
        public readonly string skytrainLineLegendFailMsg = "Skytrain Line Legend Missing";
        public static readonly By CanLineButton = By.XPath("(//*[@name='SkyTrainLine'])[1]");
        public static readonly By ExpoLineButton = By.XPath("(//*[@name='SkyTrainLine'])[2]");
        public static readonly By MillLineButton = By.XPath("(//*[@name='SkyTrainLine'])[3]");
        public static readonly By DoNotKnowButton = By.XPath("(//*[@name='SkyTrainLine'])[4]");

        //Skytrain Line Direction (Limit to Can Line and Expo Line) 
        //Canada Line 
        public static readonly By CanLineWaterfrontRadioButton = By.XPath("(//span[text() = 'Waterfront'])[1]");
        public static readonly By CanLineRichmondRadioButton = By.XPath("//span[text() = 'Richmond-Brighouse']");
        public static readonly By CanLineAirportRadioButton = By.XPath("//span[text() = 'YVR-Airport']");
        public static readonly By CanLineDoNotKnowRadioButton = By.XPath("(//*[@name='SkyTrainDirection'])[4]");

        //Expo Line 
        public static readonly By ExpLineKGRadioButton = By.XPath("//span[text() = 'King George']");
        public static readonly By ExpLinePWURadioButton = By.XPath("//span[text() = 'Production Way-University']");
        public static readonly By ExpLineWaterfrontRadioButton = By.XPath("(//span[text() = 'Waterfront'])[2]");
        public static readonly By ExpLineDoNotKnowRadioButton = By.XPath("(//*[@name='SkyTrainDirection'])[8]");

        //Skytrain Line Direction Stations (Limit to Can Line and Expo Line) 
        //Canada Line
        public static readonly By CanLineRichmondDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[1]");
        public static readonly By CanLineAirportRadioButtonDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[2]");
        public static readonly By CanLineWaterfrontDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[3]");

        //Expo Line
        public static readonly By ExpLineKGDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[4]");
        public static readonly By ExpLinePWURadioDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[5]");
        public static readonly By ExpLineWaterfrontDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[6]");

        public static readonly By STIncidentDateField = By.Id("skytrainfeedback-incidentdatetime");
        public static readonly By STIncidentTimeField = By.Id("skytrainfeedback-incidentdatetime");
        public static readonly By STPhoneNumberField = By.Id("skytrainfeedback-phonenumber");
        public static readonly By STFirstNameField = By.Id("skytrainfeedback-firstname");
        public static readonly By STCustRepResponseYesButton = By.XPath("(//*[.='Yes'])[5]");
        public static readonly By STCustRepResponseNoButton = By.XPath("(//*[.='No'])[5]");

        //Empty Required Fields Messages 
        public readonly string detailsRequiredFieldMsg = "Please enter your details in 2000 characters or less";
        public readonly string detailRequiredFieldFailMsg = "Details Required Red Text Missing";
        public readonly string nameRequiredFieldMsg = "Please enter your first name";
        public readonly string nameRequiredFieldFailMsg = "Name Required Red Text Missing";
        public readonly string emailRequiredFieldMsg = "Please enter a valid email address";
        public readonly string emailRequiredFieldFailMsg = "Email Required Red Text Missing";
    }
}
