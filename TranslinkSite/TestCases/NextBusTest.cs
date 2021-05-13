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
    //    //Next Bus ~ "NB"
    //    //Goes directly to next bus link. Does not use next bus feature on homepage 
    //    [TestCase("99", "Top"), Category("Smoke")]
    //    //[TestCase("250", "Bottom")]
    //    //[TestCase("R5", "Top")]
    //    public void NextBusRouteNumberInput(string busRoute, string destination)
    //    {
    //        NextBusPage nextBusPage = new NextBusPage(driver);
    //        nextBusPage.GoToNextBus();
    //        Assert.IsTrue((driver.FindElement(By.TagName("body")).Text.Contains(nextBusPage.nextBusPageTitle)),
    //            nextBusPage.nextBusPageTitleFailMsg);
    //        nextBusPage.EnterBusRoute(busRoute);
    //        nextBusPage.PressEnterKey();
    //        //nextBusPage.ClickFindBusRoute();

    //        nextBusPage.ChangeTimeDisplaySettings("ClockTime");
    //        nextBusPage.ChangeViewPreferenceSettings("MapView");
    //        Assert.IsTrue(driver.Url.Contains(busRoute), "Incorrect Bus Route is Displayed");
    //        Thread.Sleep(500);

    //        nextBusPage.ClickBusDestination(destination);
    //        nextBusPage.Click2ndBusStop();
    //        nextBusPage.ClickMapViewOption();
    //        nextBusPage.ChangeTimeDisplaySettings("CountDown");
    //        //Thread.Sleep(500);
    //        nextBusPage.ClickHiddenRefreshButton();
    //        //Thread.Sleep(2000); 
    //    }

    //    [TestCase("R2")]
    //    [TestCase("8")]
    //    public void NextBusBrowseDesiredRoute(string busRoute)
    //    {
    //        NextBusPage nextBusPage = new NextBusPage(driver);
    //        nextBusPage.GoToNextBus();

    //        nextBusPage.ClickBrowseAllRoutes();
    //        nextBusPage.ClickBusRoute(busRoute);
    //        nextBusPage.ClickBrowseBusDestination(busRoute);
    //        nextBusPage.ClickBrowseBusStop(busRoute);
    //        //nextBusPage.PressEnterKey();

    //        nextBusPage.ChangeTimeDisplaySettings("ClockTime");
    //        nextBusPage.ChangeViewPreferenceSettings("MapView");
    //        Assert.IsTrue(driver.Url.Contains(busRoute), "Incorrect Bus Route is Displayed");
    //        //Thread.Sleep(500);
    //        nextBusPage.ClickMapViewOption();
    //        //Thread.Sleep(500);
    //    }
    }
}
