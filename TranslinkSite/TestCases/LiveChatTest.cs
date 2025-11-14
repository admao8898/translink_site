using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    //public class LiveChatTest : UITestFixture
    //{
    //    [TestCase(), Order(1), Category("Smoke")]
    //    public void LiveChatURL()
    //    {
    //        LiveChatPage liveChatPage = new LiveChatPage(driver);
    //        liveChatPage.GoToLiveChatURL();
    //        Assert.Contains(driver.Url.Contains("livechat.translink.ca/ccmwa/chat"), "This is not the Live Chat page");

    //        //Verify Page Descriptions 
    //        Assert.Contains(driver.FindElement(By.TagName("body")).Text.Contains("To start chatting with a customer service " +
    //            "representative, please enter your name and email address below."),
    //            "Page Description is incorrect");
    //        Assert.Contains(driver.FindElement(By.TagName("body")).Text.Contains("TransLink Live Chat"),
    //            "Live Chat Page Title is incorrect"); 
    //    }

    //    [TestCase(), Order(2), Category("Smoke")]
    //    public void LiveChatVerifyDropdownOptions()
    //    {
    //        LiveChatPage liveChatPage = new LiveChatPage(driver);
    //        liveChatPage.GoToLiveChatURL();
    //        liveChatPage.VerifyTopicDropdownOptions(); 
    //    }

    //    [TestCase(), Order(3)]
    //    public void SubmitEmptyForm()
    //    {
    //        LiveChatPage liveChatPage = new LiveChatPage(driver);
    //        liveChatPage.GoToLiveChatLink(); 
    //        liveChatPage.ClickEnterChat();
    //    }

    //    [TestCase("Dean", "testEmail@noname.com", "Fares"), Order(4)]
    //    [TestCase("SkyTrain", "livechatTest@test.com", "Trip Planning")]
    //    public void FillInAllFields(string name, string email, string chatTopic)
    //    {
    //        LiveChatPage liveChatPage = new LiveChatPage(driver);
    //        liveChatPage.GoToLiveChatLink();
    //        liveChatPage.EnterFirstName(name);
    //        liveChatPage.EnterEmail(email);
    //        liveChatPage.TopicDropdownSelector(chatTopic); 
    //    }
    //}
}
