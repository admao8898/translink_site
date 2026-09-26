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

// This class is configure URL for all test cases using inheritance 
namespace TranslinkSite.TestCases
{
    public class UITestFixture
    {
        private readonly string url = "https://translink.ca/";

        public IWebDriver driver;
        private readonly string TranslinkTitle = "Welcome to TransLink";

        [SetUp]
        public void BeforeTest()
        {
            var path = System.IO.Path.GetFullPath(".");
            string browser = Environment.GetEnvironmentVariable("browser", EnvironmentVariableTarget.Process);
            string deviceType = Environment.GetEnvironmentVariable("device", EnvironmentVariableTarget.Process);
            string headlessOption = Environment.GetEnvironmentVariable("headlessValue", EnvironmentVariableTarget.Process);

            // === Browser setup ===
            // Selenium 4.38 uses Selenium Manager to automatically locate/manage
            // the appropriate browser driver when no driver is explicitly supplied.

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
            Assert.Contains(TranslinkTitle, driver.FindElement(By.XPath("//h1")).Text, "TransLink H1 is incorrect");
        }

        [TearDown]
        public void TearDown()
        {
            // Takes screenshot of all tests that fail.
            // If SetUp fails before a WebDriver session is created,
            // skip the screenshot so TearDown does not create a second failure.
            if (driver != null &&
                TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                try
                {
                    TakeScreenShot takeScreenShot = new TakeScreenShot();
                    takeScreenShot.GetFailedTestScreenshot(driver);
                }
                catch (WebDriverException ex)
                {
                    Console.WriteLine($"Could not take failure screenshot: {ex.Message}");
                }
            }

            if (driver != null)
            {
                try
                {
                    driver.Quit();
                }
                catch (WebDriverException ex)
                {
                    Console.WriteLine($"Could not quit WebDriver cleanly: {ex.Message}");
                }
                finally
                {
                    driver.Dispose();
                }
            }
        }
    }
}
