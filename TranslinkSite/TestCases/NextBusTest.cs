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
            NextBusPage nextBus = new NextBusPage(driver);
            nextBus.GoToNextBus();
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(nextBus.NextBusPageTitle)), nextBus.NextBusPageTitleErrorMsg);

            nextBus.EnterBusRoute(busroute);
            nextBus.ClickFindBusRoute();

            nextBus.ChangeSettings("ClockTime");
            Assert.IsTrue(driver.Url.Contains(busroute), "Incorrect Bus Route is Displayed");

            //nextBus.ClickTopDestination();
            nextBus.ClickBottomDestination();
            nextBus.Click2ndBusStop();

            nextBus.MapViewOption();
            Thread.Sleep(2000);

            nextBus.ClickHiddenRefreshButton();
            Thread.Sleep(2000); 
        }
    }
}
