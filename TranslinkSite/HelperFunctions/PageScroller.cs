using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TranslinkSite.HelperFunctions
{
    //Scroll page by pixels (x: vertical, y: horizontal) 
    //Parameters: up, down, left, right
    //Jan 27, 2024
    public class PageScroller
    {
        public void ScrollPageBy(IWebDriver driver, string direction, int pixels = 400)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (string.IsNullOrWhiteSpace(direction)) throw new ArgumentNullException(nameof(direction));

            var offsets = new Dictionary<string, (int x, int y)>(StringComparer.OrdinalIgnoreCase)
            {
                { "down",  (0,  pixels) },
                { "up",    (0, -pixels) },
                { "right", ( pixels, 0) },
                { "left",  (-pixels, 0) }
            };

            if (!offsets.TryGetValue(direction, out var offset))
                throw new ArgumentException("Direction must be 'up', 'down', 'left', or 'right'.", nameof(direction));

            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy({offset.x},{offset.y})");
            Console.WriteLine($"Page scrolled {direction} by {pixels} pixels");
        }
    }        
}
