using System;
using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using TranslinkSite.HelperFunctions;
using NUnit.Framework.Interfaces;

// This class is configure URL for all test cases using inheritance
namespace TranslinkSite.TestCases
{
    public class UITestFixture
    {
        private readonly string url = "https://www.translink.ca/";

        public IWebDriver driver;

        private readonly string TranslinkTitle = "Welcome to TransLink";

        [SetUp]
        public void BeforeTest()
        {
            string browser = Environment.GetEnvironmentVariable(
                "browser",
                EnvironmentVariableTarget.Process);

            string deviceType = Environment.GetEnvironmentVariable(
                "device",
                EnvironmentVariableTarget.Process);

            string headlessOption = Environment.GetEnvironmentVariable(
                "headlessValue",
                EnvironmentVariableTarget.Process);

            // ============================================================
            // Browser setup
            // ============================================================

            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArgument("--window-size=1920,1200");

            // Allow websites to use geolocation
            //
            // 1 = Allow
            // 2 = Block
            chromeOptions.AddUserProfilePreference(
                "profile.default_content_setting_values.geolocation",
                1);

            // Run headless only if explicitly requested
            if (headlessOption?.ToLower() == "true")
            {
                chromeOptions.AddArgument("headless");
                Console.WriteLine("Running in headless mode.");
            }
            else
            {
                Console.WriteLine(
                    "Running in visible (non-headless) mode.");
            }

            // ============================================================
            // Create WebDriver
            // ============================================================

            driver = browser?.ToLower() switch
            {
                "firefox" => new FirefoxDriver(),
                _ => new ChromeDriver(chromeOptions),
            };

            // ============================================================
            // Geolocation setup
            // ============================================================

            var geolocation = new GeolocationPermissionGranter();

            geolocation.GrantGeolocationPermission(
                driver,
                url);

            geolocation.SetSimulatedLocation(
                driver,
                latitude: 49.2867,
                longitude: -123.1117,
                accuracy: 10);

            // ============================================================
            // Device viewport setup
            // ============================================================

            var deviceSizes = new Dictionary<string, Size>(
                StringComparer.OrdinalIgnoreCase)
            {
                ["desktop"] = Size.Empty,
                ["Samsung_S9+"] = new Size(414, 846),
                ["Iphone11"] = new Size(414, 800)
            };

            if (deviceSizes.TryGetValue(
                    deviceType ?? "desktop",
                    out var size)
                && size != Size.Empty)
            {
                driver.Manage().Window.Size = size;

                Console.WriteLine(
                    $"Set window size to {deviceType} " +
                    $"({size.Width}x{size.Height})");
            }
            else
            {
                driver.Manage().Window.Maximize();

                Console.WriteLine(
                    "Set window to maximized (desktop view).");
            }

            // ============================================================
            // Navigate to TransLink
            // ============================================================

            driver.Navigate().GoToUrl(url);

            // ============================================================
            // Selenium timeout
            // ============================================================

            driver.Manage().Timeouts().ImplicitWait =
                TimeSpan.FromSeconds(10);

            // ============================================================
            // Validate TransLink page
            // ============================================================

            Assert.Contains(
                TranslinkTitle,
                driver.FindElement(By.XPath("//h1")).Text,
                "TransLink H1 is incorrect");
        }

        // ================================================================
        // TearDown
        // ================================================================

        [TearDown]
        public void TearDown()
        {
            // Takes screenshot of all tests that fail.
            // If SetUp fails before a WebDriver session is created,
            // skip the screenshot.

            if (driver != null &&
                TestContext.CurrentContext.Result.Outcome !=
                ResultState.Success)
            {
                try
                {
                    TakeScreenShot takeScreenShot =
                        new TakeScreenShot();

                    takeScreenShot.GetFailedTestScreenshot(driver);
                }
                catch (WebDriverException ex)
                {
                    Console.WriteLine(
                        $"Could not take failure screenshot: {ex.Message}");
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
                    Console.WriteLine(
                        $"Could not quit WebDriver cleanly: {ex.Message}");
                }
                finally
                {
                    driver.Dispose();
                }
            }
        }
    }
}