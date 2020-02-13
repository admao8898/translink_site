using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox; 
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
            string browser = Environment.GetEnvironmentVariable("browser", EnvironmentVariableTarget.Process);

            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver(path);
                    break;
                case "firefox":
                    driver = new FirefoxDriver(path);
                    break;
                default:
                    driver = new ChromeDriver(path);
                    break;
            }

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