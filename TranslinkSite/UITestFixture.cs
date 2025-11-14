using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox; 
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TranslinkSite.HelperFunctions;
using NUnit.Framework.Interfaces;
using System.Drawing;
using System.Collections.Generic;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

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
            var path = System.IO.Path.GetFullPath(".");
            string browser = Environment.GetEnvironmentVariable("browser", EnvironmentVariableTarget.Process);
            string deviceType = Environment.GetEnvironmentVariable("device", EnvironmentVariableTarget.Process);
            string headlessOption = Environment.GetEnvironmentVariable("headlessValue", EnvironmentVariableTarget.Process);

            // Automatically install the correct ChromeDriver for the Chrome version on the agent
            new DriverManager().SetUpDriver(new ChromeConfig());

            // === Browser setup ===

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--window-size=1920,1200"); // default window size

            // Run headless only if explicitly requested
            if (headlessOption?.ToLower() == "true")
            {
                chromeOptions.AddArgument("headless");
                Console.WriteLine("Running in headless mode.");
            }
            else
            {
                Console.WriteLine("Running in visible (non-headless) mode.");
            }

            // Create WebDriver instance
            driver = browser?.ToLower() switch
            {
                "firefox" => new FirefoxDriver(),
                _ => new ChromeDriver(chromeOptions),
            };

            // === Device viewport setup ===
            var deviceSizes = new Dictionary<string, Size>(StringComparer.OrdinalIgnoreCase)
            {
                ["desktop"] = Size.Empty,              // Empty means maximize
                ["Samsung_S9+"] = new Size(414, 846),
                ["Iphone11"] = new Size(414, 800)
            };

            if (deviceSizes.TryGetValue(deviceType ?? "desktop", out var size) && size != Size.Empty)
            {
                driver.Manage().Window.Size = size;
                Console.WriteLine($"Set window size to {deviceType} ({size.Width}x{size.Height})");
            }
            else
            {
                driver.Manage().Window.Maximize();
                Console.WriteLine("Set window to maximized (desktop view).");
            }

            // === Navigate and validate ===
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Assert.Contains(TranslinkTitle, driver.FindElement(By.TagName("body")).Text, "Translink Page Title is Incorrect");
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
                takeScreenShot.GetFailedTestScreenshot(driver);
            }

            driver.Close();
            driver.Quit();
        }
    }
}
    