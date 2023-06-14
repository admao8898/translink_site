using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslinkSite.Locators
{
    public class PlansProjectsPageLocators
    {

        public static readonly By HamburgerMenuButton = By.ClassName("HamburgerMenuButton");
        public static readonly By PlansProjectsLink = By.XPath("(//a[contains(text(),'Plans & Projects')])[1]"); //desktop
        public static readonly By PlansProjectsMobileTab = By.XPath("(//a[contains(text(),'Plans & Projects')])[2]"); //mobile view
        public static readonly By DesiredProjectLink = By.LinkText("TransLink Tomorrow");
        public static readonly By ProjectSearchField = By.Id("searchbox");
        public static readonly By SearchButton = By.CssSelector("div.flexContainer.flexWrapper.contentItem > button[type=\"submit\"]");

    }
}
