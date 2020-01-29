using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class TransitAlertsTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void SignUpNoValues()
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.SignUpEmpty(); 
        }
        
        [TestCase(), Order(2)]
        public void SignUpNameOnly()
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.SignUpPartialFilled(); 
        }

        [TestCase("special_character@$%!.com"), Order(3)]
        [TestCase("special~#$@sample.com")] 
        [TestCase("234@#$@email3.com")]

        public void InvalidEmailOnly(string email)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.EmailInvalidValue(email); 
        }

        [TestCase("sample88_123@nonameemail.com", "Mitch"), Order(4)]
        public void ValidEmail_Name(string email, string name)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.Email_NameValid(email, name); 
        }
    }
}
