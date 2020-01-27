using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class SignUp4AlertsTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void SignUpNoValues()
        {
            TransitAlertPageElements TransAlerts = new TransitAlertPageElements(driver);
            TransAlerts.SignUpEmpty(); 
        }
        
        [TestCase("John"), Order(2)]
        public void SignUpNameOnly(string name_value)
        {
            TransitAlertPageElements TransAlerts = new TransitAlertPageElements(driver);
            TransAlerts.SignUpPartialFilled(name_value); 
        }

        [TestCase("special_character@$%!.com"), Order(3)]
        [TestCase("special~#$@sample.com")] 
        [TestCase("234@#$@email3.com")]

        public void InvalidEmailOnly(string email)
        {
            TransitAlertPageElements TransAlerts = new TransitAlertPageElements(driver);
            TransAlerts.EmailInvalidValue(email); 
        }

        [TestCase("sample88_123@nonameemail.com", "Mitch"), Order(4)]
        public void ValidEmail_Name(string email, string name)
        {
            TransitAlertPageElements TransAlerts = new TransitAlertPageElements(driver);
            TransAlerts.Email_NameValid(email, name); 
        }
    }
}
