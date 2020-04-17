using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TranslinkSite.HelperFunctions
{
    public class DropdownListVerifier
    {
        //function verifies all values in a dropdown matches the desired values 
        public void VerifiyDropdownValues(IWebDriver driver, IWebElement element, string[] dropdownList)
        {
            SelectElement dropDownoptions = new SelectElement(element);
            IList<IWebElement> options = dropDownoptions.Options;

            //convert values in array to dictionary(hashmap)
            //order doesn't matter in dictionary
            // reference to this https://stackoverflow.com/questions/15252225/convert-an-array-to-dictionary-with-value-as-index-of-the-item-and-key-as-the-it

            var dictionary = dropdownList.Select((value, index) => new { value, index })
                .ToDictionary(pair => pair.value, pair => pair.index);

            // using a "for" loop to match all option values against desired values 
            // reference to https://stackoverflow.com/questions/9562853/how-to-get-all-options-in-a-drop-down-list-by-selenium-webdriver-using-c

            for (int i = 1; i < options.Count; i++)
            {
                var key = dictionary[options[i].GetAttribute("value")];
                //Assert.AreEqual(options[i].GetAttribute("value"), dropdownList[i], "One or more of the dropdown options are " +
                //   "missing or incorrect");
            }
        }
    }
}
