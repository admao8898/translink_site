using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;
using TranslinkSite.Locators;
using TranslinkSite.HelperFunctions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TranslinkSite.TestCases
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using System.Linq;

    public class FeedbackTest : UITestFixture
    {
        [TestCase(), Order(1)]
        public void FeedbackLink()
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            FeedbackPageLocators feedbackPageLocators = new FeedbackPageLocators();

            feedbackPage.ClickContactLink();
            feedbackPage.ClickShareYourThoughtsLink();

            // URL check
            StringAssert.Contains(driver.Url, "feedback", "This is not the Feedback page");

            // Verify page descriptions 
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.feedbackDescription, feedbackPageLocators.feedbackDescriptFailMsg);
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.dropDownTitle, feedbackPageLocators.dropDownTitleFailMsg);
        }

        [TestCase(), Order(2), Category("Smoke")]
        public void FeedbackSiteURL()
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            FeedbackPageLocators feedbackPageLocators = new FeedbackPageLocators();
            feedbackPage.GoToFeedbackSiteURL();

            // URL check
            StringAssert.Contains(driver.Url, "translink.ca/feedback", "This is not the Feedback page");

            // Verify page descriptions
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.feedbackDescription, feedbackPageLocators.feedbackDescriptFailMsg);
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.dropDownTitle, feedbackPageLocators.dropDownTitleFailMsg);
        }

        [TestCase(), Order(3), Category("Smoke")]
        public void FeedbackVerifyDropdownOptions()
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            FeedbackPageLocators feedbackPageLocators = new FeedbackPageLocators();

            feedbackPage.ClickContactLink();
            feedbackPage.ClickShareYourThoughtsLink();

            // URL check
            StringAssert.Contains(driver.Url, "feedback", "This is not the Feedback page");

            string[] dropList = { "", "LostPropertyFeedback", "BusFeedback", "SkyTrainFeedback", "SeaBusFeedback",
            "WestCoastExpressFeedback", "HandyDARTFeedback", "HandyDARTTaxiFeedback", "WebAndTechnicalFeedback", "OtherFeedback" };
            feedbackPage.VerifyAllDropdownOptions(dropList);
        }

        [TestCase("Bus"), Order(4)]
        [TestCase("SkyTrain")]
        public void FeedbackNoInput(string feedbackType)
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            FeedbackPageLocators feedbackPageLocators = new FeedbackPageLocators();

            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.SelectTypeofFeedback(feedbackType);

            switch (feedbackType)
            {
                case "Bus":
                    StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.routeNumberLegend, feedbackPageLocators.routeNumberLegendFailMsg);
                    break;

                case "SkyTrain":
                    StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.skytrainLineLegend, feedbackPageLocators.skytrainLineLegendFailMsg);
                    break;
            }

            feedbackPage.ClickSubmitButton(feedbackType);

            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.detailsRequiredFieldMsg, feedbackPageLocators.detailRequiredFieldFailMsg);
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.nameRequiredFieldMsg, feedbackPageLocators.nameRequiredFieldFailMsg);
            StringAssert.Contains(driver.FindElement(By.TagName("body")).Text, feedbackPageLocators.emailRequiredFieldMsg, feedbackPageLocators.emailRequiredFieldFailMsg);
        }

        [TestCase("Bus", "random", "R5", null, null, null, "-7", "-3", "6041234567"), Order(5)]
        [TestCase("SkyTrain", "name", null, "CanLine", "CanLineWaterfront", "Bridgeport", "0", "2", "7781234567"), Category("Smoke")]
        public void FeedbackTypePartialFilled(string feedbackType, string name, string routeNumber, string skytrainLine,
            string skytrainlineDirection, string station, string day, string time, string phoneNumber)
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.SelectTypeofFeedback(feedbackType);

            switch (feedbackType)
            {
                case "Bus":
                    feedbackPage.EnterRouteNumber(routeNumber);
                    break;

                case "SkyTrain":
                    feedbackPage.EnterSkytrainLine(skytrainLine);
                    feedbackPage.ClickSkytrainLineDirection(skytrainlineDirection);
                    feedbackPage.SelectSkyTrainStation(skytrainlineDirection, station);
                    break;
            }

            feedbackPage.EnterFirstName(name, feedbackType);
            feedbackPage.EnterIncidentDate(day, feedbackType);
            feedbackPage.EnterIncidentTime(time, feedbackType);
            feedbackPage.EnterPhoneNumber(phoneNumber, feedbackType);
            feedbackPage.EnterResponseChoice("no", feedbackType);
            feedbackPage.EnterResponseChoice("yes", feedbackType);
            feedbackPage.ClickSubmitButton(feedbackType);
        }

        [TestCase("CanLine", "CanLineWaterfront", "King Edward"), Order(6)]
        [TestCase("CanLine", "CanLineRichmond", "Lansdowne")]
        [TestCase("ExpoLine", "ExpLineKingGeorge", "Edmonds")]
        [TestCase("ExpoLine", "ExpLineWaterfront", "Stadium-Chinatown")]
        [TestCase("ExpoLine", "ExpLineProWayUni", "Main Street-Science World")]
        public void FeedbackSTLineDirectionTests(string skytrainLine, string sktyrainlineDirection, string station)
        {
            FeedbackPage feedbackPage = new FeedbackPage(driver);
            feedbackPage.GoToFeedbackSiteURL();
            feedbackPage.SelectTypeofFeedback("SkyTrain");
            feedbackPage.EnterSkytrainLine(skytrainLine);
            feedbackPage.ClickSkytrainLineDirection(sktyrainlineDirection);
            feedbackPage.SelectSkyTrainStation(sktyrainlineDirection, station);
            feedbackPage.TakeScreenShotForm();
        }
    }


    //[TestCase("green"), Order(7)]
    ////[TestCase("yellow")]
    ////[TestCase("orange")]
    ////[TestCase(null)]
    //public void FeedbackHighlighting(string highLightColour)
    //{
    //    FeedbackPage feedbackPage = new FeedbackPage(driver);
    //    feedbackPage.GoToFeedbackSiteURL();
    //    feedbackPage.HightlightText(highLightColour);
    //    //Thread.Sleep(2000); 
    //}
}
