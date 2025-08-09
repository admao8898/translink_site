using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace TranslinkSite.HelperFunctions
{
    //This function will highlight the selected text via JavaScript 
    //With reference to https://www.edgewordstraining.co.uk/2018/02/23/highlighting-web-elements/

    public class TextHighLightJS 
    {
        public void HighlightElement(IWebDriver driver, IWebElement element, string highlightColour)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (element == null) throw new ArgumentNullException(nameof(element));

            var allowedColors = new HashSet<string> { "orange", "yellow", "green" };
            if (!allowedColors.Contains(highlightColour.ToLower()))
            {
                throw new ArgumentException("Highlight colour must be orange, yellow, or green.", nameof(highlightColour));
            }

            string highlightJavascript = @"
        $(arguments[0]).css({
            'border-width': '2px',
            'border-style': 'solid',
            'border-color': 'blue',
            'background': arguments[1]
        });";

            ((IJavaScriptExecutor)driver).ExecuteScript(highlightJavascript, element, highlightColour.ToLower());
        }
    }
}
