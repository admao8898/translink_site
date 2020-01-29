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
            HomePage homePage = new HomePage(driver);
            homePage.CompassCard();
            homePage.TransitAlerts();
            homePage.TransitGenInfo(); 

        }
    }
}
