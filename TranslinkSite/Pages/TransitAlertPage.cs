using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;
using static TranslinkSite.HelperFunctions.RandomCharGenerator;
using TranslinkSite.Locators;

namespace TranslinkSite.Pages
{
    public class TransitAlertPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
            
        public TransitAlertPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        //Transit Alerts Login Page 
        public void GoToTransitAlertPage()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageLocators.TransitAlertsButton));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

        }

        //Jan 27, 2024
        //Sign Up for Transit Alerts Page 
        public void GoToTransitAlertSignUpPage()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageLocators.SignUpLink));
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
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.FindElement(TransitAlertPageLocators.NameField).SendKeys(nameValue);
                return; 
            }

            else // All other cases 
            {
                driver.FindElement(TransitAlertPageLocators.NameField).SendKeys(firstName); 
            }
        }

        public void EnterEmail(string email)
        {
            driver.FindElement(TransitAlertPageLocators.EmailField).SendKeys(email);
        }

        public void SubmitForm()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(TransitAlertPageLocators.SubmitButton));
            // can also use Actions from selenium.interactions.Actions class

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageLocators.SubmitButton));
        }
    }
}
