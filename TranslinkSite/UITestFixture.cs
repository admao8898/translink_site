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
        public readonly DateTime today = DateTime.Today; //current date 
        public readonly DateTime today_10add = DateTime.Today.AddDays(10); //current date plus 10 days ahead 
        public readonly DateTime today_7subtract = DateTime.Today.AddDays(-7); //current date subtract 7 days earlier 

        [SetUp]
        public void BeforeTest()
        {
            // gives local the execution location 
            var path = System.IO.Path.GetFullPath(".");
            driver = new ChromeDriver(path);
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            IWebElement body = driver.FindElement(By.TagName("body"));
            
            Assert.IsTrue(body.Text.Contains("Metro Vancouver's transportation network, serving residents and visitors with public transit, major roads, bridges and Trip Planning."));
            //Assert.IsTrue(body.Text.Contains("Select your school"), "Select your School Section is Missing");

        }

        [TearDown]
        public void TearDown()
        {

            driver.Close();
            driver.Quit();
        }
    }
}