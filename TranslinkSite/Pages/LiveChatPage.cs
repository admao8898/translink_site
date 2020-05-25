using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Data;
using static TranslinkSite.HelperFunctions.ExcelToDataTableConverter;
using System.Collections.Generic;
using System.Linq;
using TranslinkSite.HelperFunctions;

namespace TranslinkSite.Pages
{
    public class LiveChatPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly string liveChatURL = "https://livechat.translink.ca/";

        private static readonly By ChatWithUsLink = By.Id("chat-with-us"); 
        private static readonly By FirstNameField = By.Id("txtName");
        private static readonly By EmailField = By.Name("txtEmail");
        private static readonly By TopicDropdown = By.Id("ddlTopic");
        private static readonly By EnterChatButton = By.Id("requestchat"); 

        public LiveChatPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToLiveChatURL()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(liveChatURL);
        }

        public void GoToLiveChatLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(ChatWithUsLink));
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public void EnterFirstName(string firstName)
        {
            driver.FindElement(FirstNameField).SendKeys(firstName); 
        }

        public void EnterEmail(string email)
        {
            driver.FindElement(EmailField).SendKeys(email); 
        }

        public void TopicDropdownSelector(string topic)
        {
            new SelectElement(driver.FindElement(TopicDropdown)).SelectByText(topic);            
        }

        public void VerifyTopicDropdownOptions()
        {
            string[] dropList;
            DropdownListVerifier DropdownListVerify = new DropdownListVerifier();

            var topicDropList = ImportSheet("LiveChatDropValues.xlsx");
            dropList = topicDropList.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
 
            DropdownListVerify.VerifiyDropdownValues(driver, driver.FindElement(TopicDropdown), dropList);
        }

        public void ClickEnterChat()
        {
            driver.FindElement(EnterChatButton).Click();
        }

    }
}
