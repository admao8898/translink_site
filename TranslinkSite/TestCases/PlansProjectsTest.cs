using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

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
                Contains("Our vision is to create a better place to live built on transportation excellence. " +
                "Guided by our regional transportation strategy, we work to connect the region and enhance " +
                "its livability by providing a sustainable transportation system network."));
        }

        [TestCase(), Category("Smoke")]
        public void CheckBurnGondoLink()
        {
            PlansProjectsPage plansProjectsPage = new PlansProjectsPage(driver);
            plansProjectsPage.ClickPlansProjectsLink();
            Thread.Sleep(2000);
            plansProjectsPage.ClickBurnGondoLink();
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("TransLink is advancing " +
                "the planning and project development of a Burnaby Mountain Gondola."));
            Assert.IsTrue(driver.Url.Contains("burnaby-mountain-gondola"));
        }
    }
}
