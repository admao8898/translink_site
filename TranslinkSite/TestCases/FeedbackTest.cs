using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class FeedbackTest : UITestFixture
    {

        [TestCase(), Order(1), Category("Smoke")]
        public void FeedbackLink()
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackLink();
            Assert.IsTrue(driver.Url.Contains("translink.ca/feedback"), "This is not the Feedback page");

            //Verify Page Descriptions 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.feedbackDescription), feedbackPage.feedbackDescriptFailMsg);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.dropDownTitle), feedbackPage.dropDownTitleFailMsg);
        }

        [TestCase(), Order(2), Category("Smoke")]
        public void FeedbackSiteURL()
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            Assert.IsTrue(driver.Url.Contains("translink.ca/feedback"), "This is not the Feedback page");

            //Verify Page Descriptions 
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.feedbackDescription), feedbackPage.feedbackDescriptFailMsg);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.dropDownTitle), feedbackPage.dropDownTitleFailMsg);
        }

        [TestCase(), Order(3)]
        public void DropdownOptions()
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.VerifyAllDropdownOptions();
        }

        [TestCase("Bus"), Order(4)]
        [TestCase("SkyTrain")]
        public void FeedbackNoInput(string feedbackType)
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.SelectTypeofFeedback(feedbackType);

            switch(feedbackType)
            {
                case "Bus":
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.routeNumberLegend), feedbackPage.routeNumberLegendFailMsg);
                    feedbackPage.ClickSubmitButton(feedbackType);
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.detailsRequiredFieldMsg), feedbackPage.detailRequiredFieldFailMsg);
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.nameRequiredFieldMsg), feedbackPage.nameRequiredFieldFailMsg);
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.emailRequiredFieldMsg), feedbackPage.emailRequiredFieldFailMsg);
                    break;

                case "SkyTrain":
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.skytrainLineLegend), feedbackPage.skytrainLineLegendFailMsg);
                    feedbackPage.ClickSubmitButton(feedbackType);
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.detailsRequiredFieldMsg), feedbackPage.detailRequiredFieldFailMsg);
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.nameRequiredFieldMsg), feedbackPage.nameRequiredFieldFailMsg);
                    Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(feedbackPage.emailRequiredFieldMsg), feedbackPage.emailRequiredFieldFailMsg);
                    break;

                default:
                    return; 
                                 
            }

        }

    }
}
