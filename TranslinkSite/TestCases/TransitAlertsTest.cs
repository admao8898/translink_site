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
       
        [TestCase(null, "no"), Order(1)]
        public void SignUpEmptyForm(string NameValue, string Randomize)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.GoToSignUpForTransAlert();
            transit_alertPage.EnterFirstName(NameValue, Randomize);
            transit_alertPage.SubmitForm(); 

            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }
        
        [TestCase(null, "yes"), Order(2)]
        public void SignUpRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.GoToSignUpForTransAlert();
            transit_alertPage.EnterFirstName(NameValue, Randomize);
            transit_alertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }

        [TestCase("Jake", "no"), Order(3)]
        public void SignUpNonRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.GoToSignUpForTransAlert();
            transit_alertPage.EnterFirstName(NameValue, Randomize);
            transit_alertPage.SubmitForm();

            //Verify Field Validation and correction message 
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }


        [TestCase("special_character@$%!.com"), Order(4)]
        [TestCase("special~#$@sample.com")]
        [TestCase("234@#$@email3.com")]
        public void InvalidEmailOnly(string EmailValue)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.GoToSignUpForTransAlert();
            transit_alertPage.EnterEmail(EmailValue);
            transit_alertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);

        }

        [TestCase("sample88_123@nonameemail.com", "Mitch", "no"), Order(5)]
        public void Valid_Email_Name(string EmailValue, string NameValue, string Randomize)
        {
            TransitAlertPage transit_alertPage = new TransitAlertPage(driver);
            transit_alertPage.GoToSignUpForTransAlert();
            transit_alertPage.EnterFirstName(NameValue, Randomize);
            transit_alertPage.EnterEmail(EmailValue);
            transit_alertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedNameErrorMsg), transit_alertPage.NameErrorMsgMissing);
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedEmailErrorMsg), transit_alertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedPasswordErrorMsg), transit_alertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transit_alertPage.ExpectedTermsErrorMsg), transit_alertPage.TermsErrorMsgMissing);
        }
    }
}
