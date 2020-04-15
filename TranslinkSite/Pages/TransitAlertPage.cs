using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;
using static TranslinkSite.HelperFunctions.RandomCharGenerator; 

namespace TranslinkSite.Pages
{
    public class TransitAlertPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By TransitAlertsButton = By.LinkText("Sign up to receive transit alerts");
        private static readonly By NameField = By.Id("pagecolumnsrows_0_txtFirstName");
        private static readonly By EmailField = By.Id("pagecolumnsrows_0_txtEmail1");
        private static readonly By SubmitButton = By.Id("pagecolumnsrows_0_btnSubmit");

        // Field Validation Error Messages 
        public readonly string expectedNameFailMsg = "Please enter a first name";
        public readonly string expectedEmailFailMsg = "Please enter a valid email address";
        public readonly string expectedPasswordFailMsg = "Your password must be at least 8 characters";
        public readonly string expectedTermsFailMsg = "You must agree to the terms in order to use this service";

        public readonly string nameFailMsgMissing = "Empty First Name Message Missing";
        public readonly string emailFailMsgMissing = "Empty Email Field Message Missing";
        public readonly string passwordFailMsgMissing = "Password Field Message Missing";
        public readonly string termsFailMsgMissing = "Terms & Conditions Empty Message Missing";
        
        public TransitAlertPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToSignUpForTransAlert()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));
        }

        public void EnterFirstName(string firstName, string random)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            if (firstName == null && random == "no") // Empty Name Field 
            {
                return; 
            }

            if (firstName == null && random == "yes") // Random Name 
            {
                string nameValue = RandomWordGenerator(5, "random");
                driver.FindElement(NameField).SendKeys(nameValue);
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));
                return; 
            }

            else // All other cases 
            {
                driver.FindElement(NameField).SendKeys(firstName); 
            }
        }

        public void EnterEmail(string email)
        {
            driver.FindElement(EmailField).SendKeys(email);
        }

        public void SubmitForm()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(SubmitButton));
            // can also use Actions from selenium.interactions.Actions class

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));
        }
    }
}
