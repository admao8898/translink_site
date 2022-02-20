using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TranslinkSite.HelperFunctions;
using static TranslinkSite.HelperFunctions.DateTimeGenerator;
using static TranslinkSite.HelperFunctions.RandomCharGenerator;
using static TranslinkSite.HelperFunctions.DropdownListVerifier; 

namespace TranslinkSite.Pages
{
    public class FeedbackPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly string feedbackURL = "https://translink.ca/feedback";
        private readonly string namePipelineVariable = Environment.GetEnvironmentVariable("name", EnvironmentVariableTarget.Process);

        //Link From homepage 
        private static readonly By CustomerFeedbackLink = By.XPath("//a[.='Customer Feedback']");
        private static readonly By ShareYourThoughtsLink = By.LinkText("Share your thoughts");
        private static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        private static readonly By ContactUs = By.LinkText("Contact");


        public readonly string feedbackDescription = "We're here to help! Use this form to send us questions, " +
            "lost item inquiries, comments and suggestions.";
        public readonly string feedbackDescriptFailMsg = "Incorrect Feedback Description";
        public readonly string dropDownTitle = "What is your feedback regarding";
        public readonly string dropDownTitleFailMsg = "Incorrect Dropdown Title Displayed";

        private static readonly By FBTypeDropdownSelector = By.Name("FeedbackTopic");

        //XPath query to get nth instance of an element 
        // https://stackoverflow.com/questions/4007413/xpath-query-to-get-nth-instance-of-an-element
        private static readonly By BusFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[2]");
        private static readonly By SkyTrainFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[3]");

        private static readonly By CustomerFeedbackTitle = By.XPath("//h2[text()='Customer Feedback']");
        private static readonly By CustomerFeedbackDescription = By.XPath("");
        
        //Bus Feedback Form 
        public readonly string routeNumberLegend = "Route Number";
        public readonly string routeNumberLegendFailMsg = "Route Number Legend Missing";
        private static readonly By RouteNumberField = By.Id("busfeedback-routenumber");
        private static readonly By BusIncidentDateField = By.Id("busfeedback-incidentdatetime"); 
        private static readonly By BusIncidentTimeField = By.Id("busfeedback-incidentdatetime");
        private static readonly By BusPhoneNumberField = By.Id("busfeedback-phonenumber");
        private static readonly By BusFirstNameField = By.Id("busfeedback-firstname");
        private static readonly By BusCustRepResponseYesButton = By.XPath("(//*[.='Yes'])[3]");
        private static readonly By BusCustRepResponseNoButton = By.XPath("(//*[.='No'])[3]");

        //Skytrain FeedBack Form 
        public readonly string skytrainLineLegend = "SkyTrain Line";
        public readonly string skytrainLineLegendFailMsg = "Skytrain Line Legend Missing";
        private static readonly By CanLineButton = By.XPath("(//*[@name='SkyTrainLine'])[1]");
        private static readonly By ExpoLineButton = By.XPath("(//*[@name='SkyTrainLine'])[2]");
        private static readonly By MillLineButton = By.XPath("(//*[@name='SkyTrainLine'])[3]");
        private static readonly By DoNotKnowButton = By.XPath("(//*[@name='SkyTrainLine'])[4]");

        //Skytrain Line Direction (Limit to Can Line and Expo Line) 
        //Canada Line 
        private static readonly By CanLineWaterfrontRadioButton = By.XPath("(//span[text() = 'Waterfront'])[1]");
        private static readonly By CanLineRichmondRadioButton = By.XPath("//span[text() = 'Richmond-Brighouse']");
        private static readonly By CanLineAirportRadioButton = By.XPath("//span[text() = 'YVR-Airport']");
        private static readonly By CanLineDoNotKnowRadioButton = By.XPath("(//*[@name='SkyTrainDirection'])[4]");
        
        //Expo Line 
        private static readonly By ExpLineKGRadioButton = By.XPath("//span[text() = 'King George']");
        private static readonly By ExpLinePWURadioButton = By.XPath("//span[text() = 'Production Way-University']");
        private static readonly By ExpLineWaterfrontRadioButton = By.XPath("(//span[text() = 'Waterfront'])[2]");
        private static readonly By ExpLineDoNotKnowRadioButton = By.XPath("(//*[@name='SkyTrainDirection'])[8]");

        //Skytrain Line Direction Stations (Limit to Can Line and Expo Line) 
        //Canada Line
        private static readonly By CanLineRichmondDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[1]");
        private static readonly By CanLineAirportRadioButtonDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[2]");
        private static readonly By CanLineWaterfrontDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[3]");

        //Expo Line
        private static readonly By ExpLineKGDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[4]");
        private static readonly By ExpLinePWURadioDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[5]");
        private static readonly By ExpLineWaterfrontDropdownSelector = By.XPath("(//*[@id='skytrainfeedback-skytrainstation'])[6]");
        
        private static readonly By STIncidentDateField = By.Id("skytrainfeedback-incidentdatetime");
        private static readonly By STIncidentTimeField = By.Id("skytrainfeedback-incidentdatetime");
        private static readonly By STPhoneNumberField = By.Id("skytrainfeedback-phonenumber");
        private static readonly By STFirstNameField = By.Id("skytrainfeedback-firstname");
        private static readonly By STCustRepResponseYesButton = By.XPath("(//*[.='Yes'])[5]");
        private static readonly By STCustRepResponseNoButton = By.XPath("(//*[.='No'])[5]");

        //Empty Required Fields Messages 
        public readonly string detailsRequiredFieldMsg = "Please enter your details in 2000 characters or less";
        public readonly string detailRequiredFieldFailMsg = "Details Required Red Text Missing";
        public readonly string nameRequiredFieldMsg = "Please enter your first name";
        public readonly string nameRequiredFieldFailMsg = "Name Required Red Text Missing";
        public readonly string emailRequiredFieldMsg = "Please enter a valid email address";
        public readonly string emailRequiredFieldFailMsg = "Email Required Red Text Missing";

        public FeedbackPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void ClickContactLink()
        {
            if (driver.FindElement(HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HamburgerMenuButton).Click();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(ContactUs));
                return;
            }

            else
            {
                driver.FindElement(ContactUs).Click();
            }
        }

        public void GoToFeedbackLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(CustomerFeedbackLink));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToFeedbackSiteURL()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(feedbackURL);
        }
        
        public void ClickShareYourThoughtsLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(ShareYourThoughtsLink));
        }

        public void ClickDropdownSelector()
        {
            driver.FindElement(FBTypeDropdownSelector).Click();
        }

        public void VerifyAllDropdownOptions(string[] dropdownList)
        {
            DropdownListVerifier DropdownListVerify = new DropdownListVerifier();
            DropdownListVerify.VerifiyDropdownValues(driver, driver.FindElement(FBTypeDropdownSelector), dropdownList);
        }

        public void SelectTypeofFeedback(string type)
        {
            new SelectElement(driver.FindElement(FBTypeDropdownSelector)).SelectByText(type);
        }

        public void ClickSubmitButton(string type)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            if (type == "Bus")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(BusFeedbackSubmitButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (type == "SkyTrain")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(SkyTrainFeedbackSubmitButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            else
            {
                throw new Exception("Error: Please Include Feedback Type Value of either: Bus or SkyTrain");
            }
        }

        public void EnterSkytrainLine(string Line)
        {
            switch (Line)
            {
                case "CanLine":
                    driver.FindElement(CanLineButton).Click();
                    break;

                case "ExpoLine":
                    driver.FindElement(ExpoLineButton).Click();
                    break;

                case "MillLine":
                    driver.FindElement(MillLineButton).Click();
                    break;

                case "DoNotKnow":
                    driver.FindElement(DoNotKnowButton).Click();
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be CanLine or ExpoLine or MillLine or DoNotKnow", "Skytrain Line Type");
            }
        }

        public void ClickSkytrainLineDirection(string skytrainLineDirection)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (skytrainLineDirection)
            {
                case "CanLineWaterfront":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(CanLineWaterfrontRadioButton));
                    break;

                case "CanLineRichmond":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(CanLineRichmondRadioButton));
                    break;

                case "CanLineYVR":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(CanLineAirportRadioButton));
                    break;

                case "CanLineDoNotKnow":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(CanLineDoNotKnowRadioButton));
                    break;

                case "ExpLineKingGeorge":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(ExpLineKGRadioButton));
                    break;

                case "ExpLineProWayUni":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(ExpLinePWURadioButton));
                    break;

                case "ExpLineWaterfront":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(ExpLineWaterfrontRadioButton));
                    break;

                case "ExpLineDoNotKnow":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(ExpLineWaterfrontRadioButton));
                    break;

                default:
                    throw new System.ArgumentException("Parameter must be a valid Skytrain Line Direction", "Skytrain Line Direction Type");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void SelectSkyTrainStation(string skytrainLineDirection, string station)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (skytrainLineDirection)
            {
                //new SelectElement(driver.FindElement(SkytrainStationDropdownSelector)).SelectByText(station);
                //Need JSE b/c element not interactable 
                //https://stackoverflow.com/questions/46022541/select-element-from-dropdown-by-visible-text-using-javascript-executor

                case "CanLineWaterfront":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(CanLineWaterfrontDropdownSelector), station);
                    break;

                case "CanLineRichmond":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(CanLineRichmondDropdownSelector), station);
                    break;

                case "CanLineYVR":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(CanLineAirportRadioButtonDropdownSelector), station);
                    break;
            
                case "ExpLineKingGeorge":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(ExpLineKGDropdownSelector), station);
                    break;

                case "ExpLineProWayUni":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(ExpLinePWURadioDropdownSelector), station);
                    break;

                case "ExpLineWaterfront":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(ExpLineWaterfrontDropdownSelector), station);
                    break;

                default:
                    throw new System.ArgumentException("Parameter must be a valid Skytrain Line Direction with Corresponding Station", "Skytrain Line Direction Station Type");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void EnterRouteNumber(string routeNumber)
        {
            driver.FindElement(RouteNumberField).SendKeys(routeNumber); 
        }

        public void EnterFirstName(string nameType, string type)
        {
            if (type == "Bus")
            {
                if (nameType == "random")
                {
                    driver.FindElement(BusFirstNameField).SendKeys(RandomWordGenerator(8, nameType)); 
                    return; 
                }

                else
                {
                    driver.FindElement(BusFirstNameField).SendKeys(RandomWordGenerator(2, nameType));
                    return; 
                }               
            }

            if (type == "SkyTrain")
            {
                if (nameType == "random")
                {
                    driver.FindElement(STFirstNameField).SendKeys(RandomWordGenerator(8, nameType));
                    return;
                }

                else
                {
                    driver.FindElement(STFirstNameField).SendKeys(RandomWordGenerator(2, nameType));
                    return;
                }
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }

        public void EnterIncidentDate(string date, string type)
        {
            if (type == "Bus")
            {
                //string selectedDate = SystemDate(date); 
                driver.FindElement(BusIncidentDateField).SendKeys(SystemDate(date)); //Call DateTimeGenerator 
                return; 
            }

            if (type == "SkyTrain")
            {
                driver.FindElement(STIncidentDateField).SendKeys(SystemDate(date)); //Call DateTimeGenerator 
                return;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }

        public void EnterIncidentTime(string time, string type)
        {
            if (type == "Bus")
            {
                driver.FindElement(BusIncidentTimeField).SendKeys(SystemTime(time));
                return; 
            }

            if (type == "SkyTrain")
            {
                driver.FindElement(STIncidentTimeField).SendKeys(SystemTime(time));
                return; 
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }

        public void EnterPhoneNumber(string phoneNumber, string type)
        {
            if (type == "Bus")
            {
                driver.FindElement(BusPhoneNumberField).SendKeys(phoneNumber);
                return;
            }

            if (type == "SkyTrain")
            {
                driver.FindElement(STPhoneNumberField).SendKeys(phoneNumber);
                return;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }
        // add skytrain
        public void EnterResponseChoice(string choice, string type)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            if (choice == "yes" && type == "Bus")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(BusCustRepResponseYesButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice == "no" && type == "Bus")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(BusCustRepResponseNoButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice == "yes" && type == "SkyTrain")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(STCustRepResponseYesButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice == "no" && type == "SkyTrain")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(STCustRepResponseNoButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice != "no" || choice != "yes" || type != "Bus" || type != "SkyTrain" )
            {
                throw new Exception("Error: Please Include Response Value of either: yes or no AND Feedback Value of either: " +
                    "Bus or Skytrain");
            }
        }

        public void HightlightText(string highLightColour)
        {
            TextHighLightJS HighLighter = new TextHighLightJS();
            HighLighter.HighlightElement(driver, driver.FindElement(CustomerFeedbackTitle), highLightColour);
        }
    }

}
