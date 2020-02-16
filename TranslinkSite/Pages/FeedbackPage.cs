using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;
using static TranslinkSite.HelperFunctions.DateTimeGenerator;


namespace TranslinkSite.Pages
{
    public class FeedbackPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly string FeedbackURL = "https://new.translink.ca/feedback";
        private readonly string namePipelineVariable = Environment.GetEnvironmentVariable("name", EnvironmentVariableTarget.Process);

        //Link From homepage 
        private static readonly By CustomerFeedbackLink = By.XPath("//a[.='Customer Feedback']");

        public readonly string feedbackDescription = "We're here to help! Use this form to send us questions, lost item inquiries, comments and suggestions.";
        public readonly string feedbackDescriptFailMsg = "Incorrect Feedback Description";
        public readonly string dropDownTitle = "What is your feedback regarding";
        public readonly string dropDownTitleFailMsg = "Incorrect Dropdown Title Displayed";

        private static readonly By FBTypeDropdownSelector = By.Name("FeedbackTopic");
        private static readonly By SkytrainStationDropdownSelector = By.Id("skytrainfeedback-skytrainstation");

        //XPath query to get nth instance of an element 
        // https://stackoverflow.com/questions/4007413/xpath-query-to-get-nth-instance-of-an-element
        private static readonly By BusFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[2]");
        private static readonly By SkyTrainFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[3]");

        //Bus Feedback Form 
        public readonly string routeNumberLegend = "Route Number";
        public readonly string routeNumberLegendFailMsg = "Route Number Legend Missing";
        private static readonly By RouteNumberField = By.Id("busfeedback-routenumber");
        private static readonly By BusIncidentDateField = By.Id("busfeedback-incidentdate");
        private static readonly By BusIncidentTimeField = By.Id("busfeedback-incidenttime");
        private static readonly By BusPhoneNumberField = By.Id("busfeedback-phonenumber");
        private static readonly By BusCustRepResponseYesButton = By.XPath("(//*[.='Yes'])[3]");
        private static readonly By BusCustRepResponseNoButton = By.XPath("(//*[.='No'])[3]");

        //Skytrain FeedBack Form 
        public readonly string skytrainLineLegend = "SkyTrain Line";
        public readonly string skytrainLineLegendFailMsg = "Skytrain Line Legend Missing";
        private static readonly By CanLineButton = By.XPath("(//*[@name='SkyTrainLine'])[1]");
        private static readonly By ExpoLineButton = By.XPath("(//*[@name='SkyTrainLine'])[2]");
        private static readonly By MillLineButton = By.XPath("(//*[@name='SkyTrainLine'])[3]");
        private static readonly By DoNotKnowButton = By.XPath("(//*[@name='SkyTrainLine'])[4]");
        private static readonly By WaterStCanLineDirectionButton = By.XPath("(//span[text() = 'Waterfront'])[1]");
        private static readonly By STIncidentDateField = By.Id("skytrainfeedback-incidentdate");
        private static readonly By STIncidentTimeField = By.Id("skytrainfeedback-incidenttime");
        private static readonly By STPhoneNumberField = By.Id("skytrainfeedback-phonenumber");
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

        public void GoToFeedbackLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(CustomerFeedbackLink));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToFeedbackSiteURL()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(FeedbackURL);
        }

        public void ClickDropdownSelector()
        {
            driver.FindElement(FBTypeDropdownSelector).Click();
        }

        public void VerifyAllDropdownOptions()
        {
            SelectElement dropDownptions = new SelectElement(driver.FindElement(FBTypeDropdownSelector));
            IList<IWebElement> options = dropDownptions.Options;
            int numberofOptions = options.Count;
            string[] dropList = { "", "LostPropertyFeedback", "BusFeedback", "SkyTrainFeedback", "SeaBusFeedback", "WestCoastExpressFeedback", "HandyDARTFeedback", "HandyDARTTaxiFeedback", "WebAndTechnicalFeedback", "OtherFeedback" };
            IWebElement dropDownActualValue;
            // using a for loop to match all option values against desired values 
            // reference to https://stackoverflow.com/questions/9562853/how-to-get-all-options-in-a-drop-down-list-by-selenium-webdriver-using-c

            for (int i = 1; i < numberofOptions; i++)
            {
                dropDownActualValue = options[i];
                Assert.AreEqual(dropDownActualValue.GetAttribute("value"), dropList[i]);
            }
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
            switch(Line)
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
                    throw new System.ArgumentException("Parameter must either be CanLine or ExpoLine or MillLine or DoNotKnow", "Feedback Type");
            }
        }        

        public void ClickCanLineWaterfrontDirection()
        {            
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(WaterStCanLineDirectionButton));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void SelectSkyTrainStation(string station)
        {
            //new SelectElement(driver.FindElement(SkytrainStationDropdownSelector)).SelectByText(station);
            //Need JSE b/c element not interactable 
            //https://stackoverflow.com/questions/46022541/select-element-from-dropdown-by-visible-text-using-javascript-executor
            ((IJavaScriptExecutor)driver).ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(SkytrainStationDropdownSelector), station);

        }

        public void EnterRouteNumber(string routeNumber)
        {
            driver.FindElement(RouteNumberField).SendKeys(routeNumber); 
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
                throw new Exception("Error: Please Include Response Value of either: yes or no AND Feedback Value of either: Bus or Skytrain");
            }
        }

    }

}
