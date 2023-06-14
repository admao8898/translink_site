using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TranslinkSite.HelperFunctions;
using TranslinkSite.Locators;

namespace TranslinkSite.Pages
{
    public class PlansProjectsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        //methods
        public PlansProjectsPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        
        public void ClickPlansProjectsLink()
        {
            if (driver.FindElement(PlansProjectsPageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(PlansProjectsPageLocators.HamburgerMenuButton).Click();
                driver.FindElement(PlansProjectsPageLocators.PlansProjectsMobileTab).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(PlansProjectsPageLocators.HamburgerMenuButton).Click(); // close ham menu
                return;
            }
            
            else
            {
                driver.FindElement(PlansProjectsPageLocators.PlansProjectsLink).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }

        public void EnterProjectName(string projectName)
        {
            driver.FindElement(PlansProjectsPageLocators.ProjectSearchField).SendKeys(projectName);
        }

        public void ClickSearchButton()
        {
            //driver.FindElement(SearchButton).Click();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(PlansProjectsPageLocators.SearchButton));

        }

        public void ClickDesiredProject(string projectName)
        {
            //driver.FindElement(BurnMounGondolaLink).Click();
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(By.LinkText(projectName)));
        }

        public void TakeScreenShot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                TakeScreenShot takeScreenShot = new TakeScreenShot();
                takeScreenShot.GetRegularScreenShot(driver);
            }

            else
            {
            }
        }
    }

}
