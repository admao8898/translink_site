using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// This class is configure URL for all test cases using inheritance 
namespace TranslinkSite.TestCases
{
    public class UITestFixture
    {
        public string url = "https://new.translink.ca/";
               
        public IWebDriver driver;
        private readonly string TranslinkTitle = "Metro Vancouver's transportation network, serving residents and visitors with public transit, major roads, bridges and Trip Planning.";

        [SetUp]
        public void BeforeTest()
        {
            // gives local the execution location 
            var path = System.IO.Path.GetFullPath(".");
            driver = new ChromeDriver(path);
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);                    
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(TranslinkTitle), "Translink Page Title is Incorrect");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}