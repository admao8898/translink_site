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
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.SubmitForm(); 

            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedNameErrorMsg), transitAlertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedEmailErrorMsg), transitAlertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedPasswordErrorMsg), transitAlertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedTermsErrorMsg), transitAlertPage.TermsErrorMsgMissing);
        }
        
        [TestCase(null, "yes"), Order(2)]
        public void SignUpRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedNameErrorMsg), transitAlertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedEmailErrorMsg), transitAlertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedPasswordErrorMsg), transitAlertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedTermsErrorMsg), transitAlertPage.TermsErrorMsgMissing);
        }

        [TestCase("Jake", "no"), Order(3)]
        public void SignUpNonRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message 
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedNameErrorMsg), transitAlertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedEmailErrorMsg), transitAlertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedPasswordErrorMsg), transitAlertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedTermsErrorMsg), transitAlertPage.TermsErrorMsgMissing);
        }
        
        [TestCase("special_character@$%!.com"), Order(4)]
        [TestCase("special~#$@sample.com")]
        [TestCase("234@#$@email3.com")]
        public void InvalidEmailOnly(string EmailValue)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterEmail(EmailValue);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedNameErrorMsg), transitAlertPage.NameErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedEmailErrorMsg), transitAlertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedPasswordErrorMsg), transitAlertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedTermsErrorMsg), transitAlertPage.TermsErrorMsgMissing);

        }

        [TestCase("sample88_123@nonameemail.com", "Mitch", "no"), Order(5)]
        public void ValidEmailAndName(string EmailValue, string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.EnterEmail(EmailValue);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedNameErrorMsg), transitAlertPage.NameErrorMsgMissing);
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedEmailErrorMsg), transitAlertPage.EmailErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedPasswordErrorMsg), transitAlertPage.PasswordErrorMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.ExpectedTermsErrorMsg), transitAlertPage.TermsErrorMsgMissing);
        }
    }
}
