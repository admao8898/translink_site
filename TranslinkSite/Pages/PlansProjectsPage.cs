﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TranslinkSite.Pages
{
    public class PlansProjectsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        private static readonly By PlansProjectsLink = By.XPath("(//*[text()='Plans and Projects'])[1]"); 
        private static readonly By PlansProjectsMobileTab = By.XPath("(//*[text()='Plans and Projects'])[2]");

        //methods
        public PlansProjectsPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        
        public void ClickPlansProjectsLink()
        {
            if (driver.FindElement(HamburgerMenuButton).Displayed)
            {
                driver.FindElement(HamburgerMenuButton).Click();
                driver.FindElement(PlansProjectsMobileTab).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }
            
            else
            {
                driver.FindElement(PlansProjectsLink).Click();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
    }
}