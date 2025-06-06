using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using System.Threading;
using TranslinkSite.Pages;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TranslinkSite.TestCases
{
    public class PlansProjectsTest : UITestFixture
    {
        [TestCase()]
        public void PlansProjectPageVerification()
        {
            PlansProjectsPage plansProjectsPage = new PlansProjectsPage(driver);
            plansProjectsPage.ClickPlansProjectsLink();
            Thread.Sleep(2000);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.
                Contains("Plans and Projects"));
        }

        [TestCase("Capstan Station"), Category("Smoke")]
        [TestCase("Bus Projects")]
        [TestCase("Burnaby Mountain Gondola")]
          public void DesiredProjectLink(string project)
        {
            PlansProjectsPage plansProjectsPage = new PlansProjectsPage(driver);
            plansProjectsPage.ClickPlansProjectsLink();
            Thread.Sleep(2000);
            plansProjectsPage.EnterProjectName(project);
            plansProjectsPage.ClickSearchButton();
            plansProjectsPage.ClickDesiredProject(project); 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(project));
            Thread.Sleep(3000);
            plansProjectsPage.TakeScreenShot();
        }
    }
}
