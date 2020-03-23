using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

        [TestCase("Bus", "Bob", "R5", null, null, "today-7days","Current-3hours", "6041234567"), Order(5), Category("Smoke")]
        [TestCase("SkyTrain", "Joy", null,"CanLine", "Langara-49th Avenue", "today", "Current+3hours", "7781234567"), Category("Smoke")]
        public void FeedbackPartialFilled(string feedbackType, string name, string routeNumber,string skytrainLine, string station, string day, string time, string phoneNumber)
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.SelectTypeofFeedback(feedbackType);

            switch (feedbackType)
            {
                case "Bus":
                    feedbackPage.EnterRouteNumber(routeNumber);
                    feedbackPage.EnterIncidentDate(day, feedbackType);
                    feedbackPage.EnterIncidentTime(time, feedbackType);
                    feedbackPage.EnterPhoneNumber(phoneNumber, feedbackType);
                    feedbackPage.EnterResponseChoice("no", feedbackType);
                    Thread.Sleep(2000);
                    feedbackPage.EnterResponseChoice("yes", feedbackType);
                    feedbackPage.ClickSubmitButton(feedbackType);
                    break;

                case "SkyTrain":
                    feedbackPage.EnterSkytrainLine(skytrainLine);
                    feedbackPage.ClickCanLineWaterfrontDirection();
                    feedbackPage.SelectSkyTrainStation(station);
                    feedbackPage.EnterIncidentDate(day, feedbackType);
                    feedbackPage.EnterIncidentTime(time, feedbackType);
                    feedbackPage.EnterPhoneNumber(phoneNumber, feedbackType);
                    feedbackPage.EnterResponseChoice("no", feedbackType);
                    Thread.Sleep(2000);
                    feedbackPage.EnterResponseChoice("yes", feedbackType);
                    feedbackPage.ClickSubmitButton(feedbackType);
                    break;

                default:
                    return;
            }
        }

        [TestCase("green"), Order(6)]
        [TestCase("yellow")]
        [TestCase("orange")]
        [TestCase(null)]
        public void FeedbackHighlighting(string highLightColour)
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.HightlightText(highLightColour); 
        }
    }
}
