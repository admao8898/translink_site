using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox; 
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing.Imaging;
using TranslinkSite.HelperFunctions;
using NUnit.Framework.Interfaces;
using System.Drawing;
using System.Collections.Generic;

// This class is configure URL for all test cases using inheritance 
namespace TranslinkSite.TestCases
{
    public class UITestFixture
    {
        private readonly string url = "https://translink.ca/";

        public IWebDriver driver;
        private readonly string TranslinkTitle = "Metro Vancouver's transportation network, serving residents and visitors " +
            "with public transit, major roads, bridges and Trip Planning.";

        
        [SetUp]
        public void BeforeTest()
        {
            // gives local the execution location 
            var path = System.IO.Path.GetFullPath(".");
            string browser = Environment.GetEnvironmentVariable("browser", EnvironmentVariableTarget.Process);
            string deviceType = Environment.GetEnvironmentVariable("device", EnvironmentVariableTarget.Process);
            string headlessOption = Environment.GetEnvironmentVariable("headlessValue", EnvironmentVariableTarget.Process);

            var chromeOptions = new ChromeOptions();

            // because invisible window size is only 800x600, need to set desired screen size
            // reference to https://itnext.io/how-to-run-a-headless-chrome-browser-in-selenium-webdriver-c5521bc12bf0
            chromeOptions.AddArguments("headless", "--window-size=1920,1200");

            driver = headlessOption switch
            {
                "true" => new ChromeDriver(chromeOptions),
                "false" => browser switch
                {
                    "chrome" => new ChromeDriver(path),
                    "firefox" => new FirefoxDriver(path),
                    _ => new ChromeDriver(path), //default expression 
                },
                _ => new ChromeDriver(chromeOptions), //default expression 
                //_ => new ChromeDriver(path),
            };

            //switch (headlessOption)
            //{
            //    case "true":
            //        driver = new ChromeDriver(chromeOptions);
            //        break;

            //    case "false":
            //        driver = browser switch
            //        {
            //            "chrome" => new ChromeDriver(path),
            //            "firefox" => new FirefoxDriver(path),
            //            _ => new ChromeDriver(path),
            //        };
            //        break;

            //    default:
            //        driver = new ChromeDriver(chromeOptions);
            //        //driver = new ChromeDriver(path);
            //        break;
            //}

            switch (deviceType)
            {
                case "desktop":
                    driver.Manage().Window.Maximize(); // desktop view
                    break;
                case "Samsung_S9+":
                    driver.Manage().Window.Size = new Size(414, 846); // set window size to Samsung S9 size 
                    break;
                case "Iphone11":
                    driver.Manage().Window.Size = new Size(414, 800); // approximated 
                    break;
                default:
                    driver.Manage().Window.Maximize(); // desktop view
                    //driver.Manage().Window.Size = new Size(414, 800); // approximated 
                    break;
            }

            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(TranslinkTitle), "Translink Page Title is Incorrect");
        }

        [TearDown]
        public void TearDown()
        {
            //Takes screenshot of all tests that fail
            //Reference to https://stackoverflow.com/questions/44287058/error-on-taking-screenshot-in-selenium-c-sharp
            //Use try catch in future https://stackoverflow.com/questions/14973642/how-using-try-catch-for-exception-handling-is-best-practice
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                TakeScreenShot takeScreenShot = new TakeScreenShot();
                takeScreenShot.GetFailedTestScreenShot(driver);
            }

            driver.Close();
            driver.Quit();
        }
    }
}
    