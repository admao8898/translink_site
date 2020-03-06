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
        [TestCase("R4", "Top"), Order(1)]
        [TestCase("145", "Bottom")]
        [TestCase("555", "Top")]
        public void NextBusRoute(string busRoute, string destination)
        {
            NextBusPage nextBusPage = new NextBusPage(driver);
            nextBusPage.GoToNextBus();
            Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(nextBusPage.nextBusPageTitle)), nextBusPage.nextBusPageTitleFailMsg);

            nextBusPage.EnterBusRoute(busRoute);
            nextBusPage.ClickFindBusRoute();

            nextBusPage.ChangeTimeDisplaySettings("ClockTime");
            nextBusPage.ChangeViewPreferenceSettings("MapView"); 
            Assert.IsTrue(driver.Url.Contains(busRoute), "Incorrect Bus Route is Displayed");
            nextBusPage.Destination(destination);
            Thread.Sleep(500); 
            nextBusPage.Click2ndBusStop();
            Thread.Sleep(2000);
            nextBusPage.MapViewOption();

            nextBusPage.ClickHiddenRefreshButton();
            Thread.Sleep(2000); 
        }
    }
}
