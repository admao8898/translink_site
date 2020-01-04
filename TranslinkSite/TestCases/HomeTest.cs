using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TranslinkSite.Pages; 

namespace TranslinkSite.TestCases
{
    public class HomeTest : UITestFixture
    {
        [TestCase(), Order(1)]

        public void VerifyPageContents()
        {
            HomePageElements HomePageNav = new HomePageElements(driver); 
            //HomePageNav.CompassCard();
            //HomePageNav.TransitAlerts();
            HomePageNav.TransitGenInfo(); 

        }
    }
}
