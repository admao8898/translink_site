﻿using System;
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
        public void GetFailedTestScreenShot(IWebDriver driver) //For test cases that failed
        {
            var screenshot = driver.TakeScreenshot();
            // testcontext class https://docs.nunit.org/articles/nunit/writing-tests/TestContext.html
            string testMethodName = TestContext.CurrentContext.Test.MethodName + "_Failed_";
            string fileName = testMethodName + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";
            string screenshotFile = Path.Combine(Environment.CurrentDirectory, fileName);
            screenshot.SaveAsFile(screenshotFile);
            TestContext.AddTestAttachment(screenshotFile, "My Screenshot");            
        }

        public void GetRegularScreenShot(IWebDriver driver) //Non Failed test cases 
        {
            var screenshot = driver.TakeScreenshot();
            string testMethodName = TestContext.CurrentContext.Test.MethodName + "_ScreenShot_";
            string fileName = testMethodName + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";
            string screenshotFile = Path.Combine(Environment.CurrentDirectory, fileName);
            screenshot.SaveAsFile(screenshotFile);
            TestContext.AddTestAttachment(screenshotFile, "My Screenshot");
        }
    }
}
