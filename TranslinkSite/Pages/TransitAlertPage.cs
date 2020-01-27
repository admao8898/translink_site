using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.TestCases;

namespace TranslinkSite.Pages
{
    public class TransitAlertPageElements
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By TransitAlertsButton = By.LinkText("Sign up to receive transit alerts");
        private static readonly By NameField = By.Id("pagecolumnsrows_0_txtFirstName");
        private static readonly By EmailField = By.Id("pagecolumnsrows_0_txtEmail1");
        private static readonly By SubmitButton = By.Id("pagecolumnsrows_0_btnSubmit"); 

        public TransitAlertPageElements(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void SignUpEmpty()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
           
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageElements.TransitAlertsButton));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageElements.SubmitButton));

            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a first name")), "Empty First Name Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a valid email address")), "Empty Email Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Your password must be at least 8 characters")), "Password Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("You must agree to the terms in order to use this service")), "Terms & Conditions Empty Message Missing");

        }

        public void SignUpPartialFilled(string name)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageElements.TransitAlertsButton));

            driver.FindElement(TransitAlertPageElements.NameField).SendKeys(name);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageElements.SubmitButton));

            //Verify all other Fields contain validation messages other than first name field 
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a valid email address")), "Empty Email Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Your password must be at least 8 characters")), "Password Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("You must agree to the terms in order to use this service")), "Terms & Conditions Empty Message Missing");
        }

        public void EmailInvalidValue(string email)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));

            driver.FindElement(TransitAlertPageElements.EmailField).SendKeys(email);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));

            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a valid email address")), "Empty Email Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Your password must be at least 8 characters")), "Password Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("You must agree to the terms in order to use this service")), "Terms & Conditions Empty Message Missing");

        }

        public void Email_NameValid(string email, string name)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertsButton));

            driver.FindElement(NameField).SendKeys(name); 
            driver.FindElement(EmailField).SendKeys(email);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(SubmitButton));
            
            Assert.IsFalse((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a valid email address")), "Empty Email Field Message Missing");
            Assert.IsFalse((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a first name")), "Empty First Name Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Your password must be at least 8 characters")), "Password Field Message Missing");
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("You must agree to the terms in order to use this service")), "Terms & Conditions Empty Message Missing");

        }
    }
}
