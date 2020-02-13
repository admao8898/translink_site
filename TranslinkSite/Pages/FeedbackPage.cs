using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;

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

        private static readonly By DropdownSelector = By.Name("FeedbackTopic");

        //XPath query to get nth instance of an element 
        // https://stackoverflow.com/questions/4007413/xpath-query-to-get-nth-instance-of-an-element
        private static readonly By BusFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[2]");
        private static readonly By SkyTrainFeedbackSubmitButton = By.XPath("(//*[.='Submit'])[3]");

        //Bus Feedback Form 
        public readonly string routeNumberLegend = "Route Number"; 
        public readonly string routeNumberLegendFailMsg = "Route Number Legend Missing";

        //Skytrain FeedBack Form 
        public readonly string skytrainLineLegend = "SkyTrain Line";
        public readonly string skytrainLineLegendFailMsg = "Skytrain Line Legend Missing";

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
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(CustomerFeedbackLink));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToFeedbackSiteURL()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(FeedbackURL);
        }

        public void ClickDropdownSelector()
        {
            driver.FindElement(DropdownSelector).Click(); 
        }

        public void VerifyAllDropdownOptions()
        {
            SelectElement dropDownptions = new SelectElement(driver.FindElement(DropdownSelector));
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
            new SelectElement(driver.FindElement(DropdownSelector)).SelectByText(type); 
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
    }
}
