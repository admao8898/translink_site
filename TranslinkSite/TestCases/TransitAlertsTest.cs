using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPage.nameFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedEmailFailMsg), transitAlertPage.emailFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
        }
        
        [TestCase(null, "yes"), Order(2), Category("Special")]
        public void SignUpRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            Thread.Sleep(2000);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPage.nameFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedEmailFailMsg), transitAlertPage.emailFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
        }

        [TestCase("Jake", "no"), Order(3)]
        public void SignUpNonRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            transitAlertPage.GoToSignUpForTransAlert();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message 
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPage.nameFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedEmailFailMsg), transitAlertPage.emailFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
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
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPage.nameFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedEmailFailMsg), transitAlertPage.emailFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);

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
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPage.nameFailMsgMissing);
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedEmailFailMsg), transitAlertPage.emailFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
        }
    }
}
