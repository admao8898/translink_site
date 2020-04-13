using OpenQA.Selenium;

namespace TranslinkSite.HelperFunctions
{
    //This function will highlight the selected text via JavaScript 
    //With reference to https://www.edgewordstraining.co.uk/2018/02/23/highlighting-web-elements/

    public class TextHighLightJS 
    {
        public void HighlightElement(IWebDriver driver, IWebElement element, string highLightColour) // function will need input value of driver otherwise just reads null 
        {
            if (highLightColour == "orange")
            {
                string highlightJavascript = @"$(arguments[0]).css({ ""border-width"" : ""2px"", ""border-style"" : ""solid"", ""border-color"" : ""blue"", ""background"" : ""orange"" });";
                ((IJavaScriptExecutor)driver).ExecuteScript(highlightJavascript, new object[] { element });
                return; 
            }

            if (highLightColour == "yellow")
            {
                string highlightJavascript = @"$(arguments[0]).css({ ""border-width"" : ""2px"", ""border-style"" : ""solid"", ""border-color"" : ""blue"", ""background"" : ""yellow"" });";
                ((IJavaScriptExecutor)driver).ExecuteScript(highlightJavascript, new object[] { element });
                return;
            }

            if (highLightColour == "green")
            {
                string highlightJavascript = @"$(arguments[0]).css({ ""border-width"" : ""2px"", ""border-style"" : ""solid"", ""border-color"" : ""blue"", ""background"" : ""green"" });";
                ((IJavaScriptExecutor)driver).ExecuteScript(highlightJavascript, new object[] { element });
                return;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be orange or yellow or green", "Highlight Colour Type");
            }           
        }
    }
}
