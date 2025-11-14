using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TranslinkSite.TestCases
{
    public class SchedulesTest : UITestFixture
    {
        [TestCase(), Category("Smoke")]
        public void VerifySchedulePage()
        {
            SchedulesPage schedulesPage = new SchedulesPage(driver);
            schedulesPage.GoToSchedulesPage();
            Assert.Contains(driver.FindElement(By.TagName("body")).Text.
                Contains("Find schedules and maps for bus, SeaBus, SkyTrain, and West Coast Express."),
                "This is not the Schedules Page" );
        }
    }
}
