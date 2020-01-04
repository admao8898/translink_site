using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class SignUp4Alerts : UITestFixture
    {
        [TestCase(), Order(1)]
        public void SignUpNoValues()
        {
            TransitAlertPageElements TransAlerts = new TransitAlertPageElements(driver);
            TransAlerts.SignUpEmpty(); 
        }
    }
}
