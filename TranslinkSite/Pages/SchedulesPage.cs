using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TranslinkSite.Pages;

namespace TranslinkSite.Pages
{
    public class SchedulesPage
    {
        private readonly IWebDriver driver;

        //siteURL 
        private readonly string SchedulesURL = "https://new.translink.ca/schedules-and-maps";

        //Schedule Type 
        private static readonly By BusSchContainFullScreen = By.XPath("(//*[@href='/schedules-and-maps/bus-schedules'])[2]");
        private static readonly By BusSchContainMobile = By.XPath("(//*[@href='/schedules-and-maps/bus-schedules'])[1]"); 
        public SchedulesPage(IWebDriver drv)
        {
            driver = drv; 
        }

        public void GoToSchedulesPage()
        {
            driver.Navigate().GoToUrl(SchedulesURL);
        }

        public void GoToScheduleChoice(string type)
        {
            switch(type)
            {
                case "bus":
                    driver.FindElement(BusSchContainFullScreen).Click();
                    break;

                default:
                    break; 
            }
                
        }
    }
}
