using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
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
            var locator = By.LinkText(projectName);

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    // Wait until present & clickable (but do NOT store the WebElement)
                    wait.Until(SeleniumExtras.WaitHelpers
                        .ExpectedConditions.ElementIsVisible(locator));

                    // Re-locate a fresh element every retry
                    var element = driver.FindElement(locator);

                    // Scroll into view to avoid click issues when below the fold
                    ((IJavaScriptExecutor)driver).ExecuteScript(
                        "arguments[0].scrollIntoView({block: 'center'});",
                        element);

                    // Wait briefly for scroll animation/layout
                    Thread.Sleep(200);

                    // JS click avoids overlay & stale timing issues
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);

                    return; // SUCCESS
                }
                catch (StaleElementReferenceException)
                {
                    // DOM refreshed — retry
                    Thread.Sleep(150);
                }
                catch (ElementClickInterceptedException)
                {
                    // Scroll + layout shift — retry
                    Thread.Sleep(150);
                }
            }

            throw new Exception($"Failed to click project '{projectName}'. It may not be visible or the DOM keeps refreshing.");
        }
        public void TakeScreenShot()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                TakeScreenShot takeScreenShot = new TakeScreenShot();
                takeScreenShot.GetRegularScreenshot(driver);
            }

            else
            {
            }
        }
    }

}
