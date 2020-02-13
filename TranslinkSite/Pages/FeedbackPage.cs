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
        
        //Link From homepage 
        private static readonly By CustomerFeedbackLink = By.XPath("//a[.='Customer Feedback']");

        public readonly string feedbackDescription = "We're here to help! Use this form to send us questions, lost item inquiries, comments and suggestions.";
        public readonly string feedbackDescriptErrorMsg = "Incorrect Feedback Description";
        public readonly string dropDownTitle = "What is your feedback regarding";
        public readonly string dropDownTitleErrorMsg = "Incorrect Dropdown Title Displayed";

        private static readonly By DropdownSelector = By.Name("FeedbackTopic");

        //Bus Feedback Form 
        public readonly string routeNumberLegend = "Route Number"; 
        public readonly string routeNumberLegendErrorMsg = "Route Number Legend Missing";

        //Skytrain FeedBack Form 
        public readonly string skytrainLineLegend = "SkyTrain Line";
        public readonly string skytrainLineLegendErrorMsg = "Skytrain Line Legend Missing";

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


    }
}
