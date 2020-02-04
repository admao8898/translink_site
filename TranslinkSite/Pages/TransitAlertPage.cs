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
        public readonly string ExpectedNameErrorMsg = "Please enter a first name";
        public readonly string ExpectedEmailErrorMsg = "Please enter a valid email address";
        public readonly string ExpectedPasswordErrorMsg = "Your password must be at least 8 characters";
        public readonly string ExpectedTermsErrorMsg = "You must agree to the terms in order to use this service";

        public readonly string NameErrorMsgMissing = "Empty First Name Message Missing";
        public readonly string EmailErrorMsgMissing = "Empty Email Field Message Missing";
        public readonly string PasswordErrorMsgMissing = "Password Field Message Missing";
        public readonly string TermsErrorMsgMissing = "Terms & Conditions Empty Message Missing";

        public TransitAlertPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void SignUpEmpty()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);           
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));          

        }

        public void SignUpPartialFilled()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));

            string namevalue = RandomWordGenerator(3, "random" ); 
            driver.FindElement(NameField).SendKeys(namevalue);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));
            
        }

        public void EmailInvalidValue(string email)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));

            driver.FindElement(EmailField).SendKeys(email);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));

           
        }

        public void Email_NameValid(string email, string name)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));

            driver.FindElement(NameField).SendKeys(name); 
            driver.FindElement(EmailField).SendKeys(email);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));
            
        }


    }
}
