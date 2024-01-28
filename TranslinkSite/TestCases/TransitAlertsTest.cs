using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;
using TranslinkSite.Locators;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// as of Dec 1, 2020 alerts is down due to ransomware attack 
// will pause these tests until further notice 
namespace TranslinkSite.TestCases
{
    public class TransitAlertsTest : UITestFixture
    {
        [TestCase(null, "yes"), Order(1), Category("Smoke")]
        public void TAlertSignUpEmptyForm(string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            TransitAlertPageLocators transitAlertPageLocators = new();
            transitAlertPage.GoToTransitAlertPage();
            Thread.Sleep(2000);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedSignInTransitAlertsAccountHeader), 
            transitAlertPageLocators.signTranAlertHeaderFailMsg);
            transitAlertPage.GoToTransitAlertSignUpPage();
            Thread.Sleep(2000);
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            
            //Verify Field Validation and correction message  
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedNameFailMsg), transitAlertPageLocators.nameFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedEmailFailMsg), transitAlertPageLocators.emailFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedPasswordFailMsg), transitAlertPageLocators.passwordFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedTermsFailMsg), transitAlertPageLocators.termsFailMsgMissing);
        }

        [TestCase(null, "yes"), Order(2)]
        public void TAlertSignUpRandomNameOnly(string NameValue, string Randomize)
        {
            //var task = Task.Factory.StartNew(() =>
            //{
            //    TransitAlertPage transitAlertPage = new TransitAlertPage(driver);

            //    transitAlertPage.GoToSignUpForTransAlert();
            //    transitAlertPage.EnterFirstName(NameValue, Randomize);
            //    Task.Delay(2000).Wait();

            //    transitAlertPage.SubmitForm();
            //    Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPageLocators.nameFailMsgMissing);

            //});

            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            TransitAlertPageLocators transitAlertPageLocators = new();

            transitAlertPage.GoToTransitAlertPage();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            Thread.Sleep(2000);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedNameFailMsg), transitAlertPageLocators.nameFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedEmailFailMsg), transitAlertPageLocators.emailFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
        }

        [TestCase("Jake", "no"), Order(3)]
        public void TAlertSignUpNonRandomNameOnly(string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            TransitAlertPageLocators transitAlertPageLocators = new();
            transitAlertPage.GoToTransitAlertPage();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message 
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedNameFailMsg), transitAlertPageLocators.nameFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedEmailFailMsg), transitAlertPageLocators.emailFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
        }

        [TestCase("special_character@$%!.com"), Order(4)]
        //[TestCase("special~#$@sample.com")]
        [TestCase("234@#$@email3.com")]
        public void TAlertInvalidEmailOnly(string EmailValue)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            TransitAlertPageLocators transitAlertPageLocators = new();
            transitAlertPage.GoToTransitAlertPage();
            transitAlertPage.EnterEmail(EmailValue);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message  
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedNameFailMsg), transitAlertPage.nameFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedEmailFailMsg), transitAlertPageLocators.emailFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedPasswordFailMsg), transitAlertPage.passwordFailMsgMissing);
            //Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPage.expectedTermsFailMsg), transitAlertPage.termsFailMsgMissing);
        }

        [TestCase("sample88_123@nonameemail.com", "Alex", "no"), Order(5), Category("Smoke")]
        public void TAlertValidEmailAndName(string EmailValue, string NameValue, string Randomize)
        {
            TransitAlertPage transitAlertPage = new TransitAlertPage(driver);
            TransitAlertPageLocators transitAlertPageLocators = new();
            transitAlertPage.GoToTransitAlertPage();
            transitAlertPage.EnterFirstName(NameValue, Randomize);
            transitAlertPage.EnterEmail(EmailValue);
            transitAlertPage.SubmitForm();

            //Verify Field Validation and correction message  
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedNameFailMsg), transitAlertPageLocators.nameFailMsgMissing);
            Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedEmailFailMsg), transitAlertPageLocators.emailFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedPasswordFailMsg), transitAlertPageLocators.passwordFailMsgMissing);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(transitAlertPageLocators.expectedTermsFailMsg), transitAlertPageLocators.termsFailMsgMissing);
        }
    }
}
