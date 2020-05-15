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
    public class TakeScreenShot
    {
        public void GetScreenShot(IWebDriver driver)
        {
            var screenshot = driver.TakeScreenshot();
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "_error_screenshot.png";
            string screenshotFile = Path.Combine(Environment.CurrentDirectory, fileName);
            screenshot.SaveAsFile(screenshotFile, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(screenshotFile, "My Screenshot");
        }
    }
}
