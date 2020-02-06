using System;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using NUnit.Framework;
using OpenQA.Selenium; 
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class NextBusTest : UITestFixture
    {
        //Next Bus ~ "NB"
        //Goes directly to next bus link. Does not use next bus feature on homepage 
        [TestCase("R5"), Order(1)]
        [TestCase("99")]
        [TestCase("145")]
        public void NextBusRoute(string busroute)
        {
            NextBusPage nextBusPage = new NextBusPage(driver);
            nextBusPage.GoToNextBus();
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(nextBusPage.NextBusPageTitle)), nextBusPage.NextBusPageTitleErrorMsg);

            nextBusPage.EnterBusRoute(busroute);
            nextBusPage.ClickFindBusRoute();

            nextBusPage.ChangeSettings("ClockTime");
            Assert.IsTrue(driver.Url.Contains(busroute), "Incorrect Bus Route is Displayed");

            //nextBusPage.ClickTopDestination();
            nextBusPage.ClickBottomDestination();
            nextBusPage.Click2ndBusStop();

            nextBusPage.MapViewOption();
            Thread.Sleep(2000);

            nextBusPage.ClickHiddenRefreshButton();
            Thread.Sleep(2000); 
        }
    }
}
