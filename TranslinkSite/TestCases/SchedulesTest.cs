using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class SchedulesTest : UITestFixture
    {
        [TestCase(), Category("Smoke")]
        public void VerifySchedulePage()
        {
            SchedulesPage schedulesPage = new(driver);
            schedulesPage.GoToSchedulesPage();

            // Verify the page body contains the expected text
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text,
                                  "Find schedules and maps for bus, SeaBus, SkyTrain, and West Coast Express.",
                                  "This is not the Schedules Page");
        }

    }
}
