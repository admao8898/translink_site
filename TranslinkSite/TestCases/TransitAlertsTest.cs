using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
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

            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }
        
        [TestCase(), Order(2)]
        public void SignUpNameOnly()
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.SignUpPartialFilled();

            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }

        [TestCase("special_character@$%!.com"), Order(3)]
        [TestCase("special~#$@sample.com")]
        [TestCase("234@#$@email3.com")]
        public void InvalidEmailOnly(string email)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.EmailInvalidValue(email);
           
            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);

        }

        [TestCase("sample88_123@nonameemail.com", "Mitch"), Order(4)]
        public void ValidEmail_Name(string email, string name)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.Email_NameValid(email, name);

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }
    }
}
