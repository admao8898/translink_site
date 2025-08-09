using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing.Imaging;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.IO;
using NUnit.Framework;

namespace TranslinkSite.HelperFunctions
{
    // using screen shot 
    // https://stackoverflow.com/questions/33320912/take-screenshot-on-test-failure-exceptions

    public class TakeScreenShot
    {
        public void CaptureScreenshot(IWebDriver driver, string label)
        {
            var screenshot = driver.TakeScreenshot();
            var testMethodName = $"{TestContext.CurrentContext.Test.MethodName}_{label}_";
            var fileName = $"{testMethodName}{DateTime.Now:yyyy-MM-dd HH-mm-ss}.png";
            var screenshotFile = Path.Combine(Environment.CurrentDirectory, fileName);

            screenshot.SaveAsFile(screenshotFile);
            TestContext.AddTestAttachment(screenshotFile, "My Screenshot");
        }

        public void GetFailedTestScreenshot(IWebDriver driver)
        {
            CaptureScreenshot(driver, "Failed");
        }

        public void GetRegularScreenshot(IWebDriver driver)
        {
            CaptureScreenshot(driver, "ScreenShot");
        }
    }
}
