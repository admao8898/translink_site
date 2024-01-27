using System;
using OpenQA.Selenium;

namespace TranslinkSite.HelperFunctions
{
    //Scroll page by pixels (x: vertical, y: horizontal) 
    //Parameters: up, down, left, right
    //Jan 27, 2024
    public class PageScroller
    {
        public void ScrollPageBy(IWebDriver driver,string direction)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (direction)
            {
                case "down":
                    jse.ExecuteScript("window.scrollBy(0,400)", "");
                    Console.WriteLine("Page Scrolled Down by 400 Pixels");
                    break;

                case "up":
                    jse.ExecuteScript("window.scrollBy(0,-400)", "");
                    Console.WriteLine("Page Scrolled Up by 400 Pixels");
                    break;

                case "left":
                    jse.ExecuteScript("window.scrollBy(400,0)", "");
                    Console.WriteLine("Page Scrolled left by 400 Pixels");
                    break;

                case "right":
                    jse.ExecuteScript("window.scrollBy(-400,0)", "");
                    Console.WriteLine("Page Scrolled right by 400 Pixels");
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be up, down, left, right", "Scroll Page Direction");
            }

        }
    }        
}
