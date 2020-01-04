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
           
            //IWebElement SignUpForAlerts = driver.FindElement(TransitAlertPageElements.TransitAlertsButton);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageElements.TransitAlertsButton));

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //IWebElement SubmitButton = driver.FindElement(TransitAlertPageElements.SubmitButton);
            jse.ExecuteScript("arguments[0].click()", driver.FindElement(TransitAlertPageElements.SubmitButton));

            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains("Please enter a first name")), "Empty First Name Message Missing"); 
        }

    }
}
