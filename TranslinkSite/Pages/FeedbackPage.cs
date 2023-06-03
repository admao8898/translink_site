using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TranslinkSite.HelperFunctions;
using TranslinkSite.Locators; 
using static TranslinkSite.HelperFunctions.DateTimeGenerator;
using static TranslinkSite.HelperFunctions.RandomCharGenerator;
using static TranslinkSite.HelperFunctions.DropdownListVerifier; 

namespace TranslinkSite.Pages
{
    public class FeedbackPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public FeedbackPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void ClickContactLink()
        {
            if (driver.FindElement(FeedbackPageLocators.HamburgerMenuButton).Displayed)
            {
                driver.FindElement(FeedbackPageLocators.HamburgerMenuButton).Click();
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.ContactUs));
                return;
            }

            else
            {
                driver.FindElement(FeedbackPageLocators.ContactUs).Click();
            }
        }

        public void GoToFeedbackLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.CustomerFeedbackLink));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToFeedbackSiteURL()
        {
            FeedbackPageLocators feedBackPageLocators = new FeedbackPageLocators();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(feedBackPageLocators.feedbackURL);
        }
        
        public void ClickShareYourThoughtsLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.ShareYourThoughtsLink));
        }

        public void ClickDropdownSelector()
        {
            driver.FindElement(FeedbackPageLocators.FBTypeDropdownSelector).Click();
        }

        public void VerifyAllDropdownOptions(string[] dropdownList)
        {
            DropdownListVerifier DropdownListVerify = new DropdownListVerifier();
            DropdownListVerify.VerifiyDropdownValues(driver, driver.FindElement(FeedbackPageLocators.FBTypeDropdownSelector), dropdownList);
        }

        public void SelectTypeofFeedback(string type)
        {
            new SelectElement(driver.FindElement(FeedbackPageLocators.FBTypeDropdownSelector)).SelectByText(type);
        }

        public void ClickSubmitButton(string type)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            if (type == "Bus")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.BusFeedbackSubmitButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (type == "SkyTrain")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.SkyTrainFeedbackSubmitButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            else
            {
                throw new Exception("Error: Please Include Feedback Type Value of either: Bus or SkyTrain");
            }
        }

        public void EnterSkytrainLine(string Line)
        {
            switch (Line)
            {
                case "CanLine":
                    driver.FindElement(FeedbackPageLocators.CanLineButton).Click();
                    break;

                case "ExpoLine":
                    driver.FindElement(FeedbackPageLocators.ExpoLineButton).Click();
                    break;

                case "MillLine":
                    driver.FindElement(FeedbackPageLocators.MillLineButton).Click();
                    break;

                case "DoNotKnow":
                    driver.FindElement(FeedbackPageLocators.DoNotKnowButton).Click();
                    break;

                default:
                    throw new System.ArgumentException("Parameter must either be CanLine or ExpoLine or MillLine or DoNotKnow", "Skytrain Line Type");
            }
        }

        public void ClickSkytrainLineDirection(string skytrainLineDirection)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (skytrainLineDirection)
            {
                case "CanLineWaterfront":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.CanLineWaterfrontRadioButton));
                    break;

                case "CanLineRichmond":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.CanLineRichmondRadioButton));
                    break;

                case "CanLineYVR":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.CanLineAirportRadioButton));
                    break;

                case "CanLineDoNotKnow":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.CanLineDoNotKnowRadioButton));
                    break;

                case "ExpLineKingGeorge":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.ExpLineKGRadioButton));
                    break;

                case "ExpLineProWayUni":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.ExpLinePWURadioButton));
                    break;

                case "ExpLineWaterfront":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.ExpLineWaterfrontRadioButton));
                    break;

                case "ExpLineDoNotKnow":
                    jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.ExpLineWaterfrontRadioButton));
                    break;

                default:
                    throw new System.ArgumentException("Parameter must be a valid Skytrain Line Direction", "Skytrain Line Direction Type");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void SelectSkyTrainStation(string skytrainLineDirection, string station)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            switch (skytrainLineDirection)
            {
                //new SelectElement(driver.FindElement(SkytrainStationDropdownSelector)).SelectByText(station);
                //Need JSE b/c element not interactable 
                //https://stackoverflow.com/questions/46022541/select-element-from-dropdown-by-visible-text-using-javascript-executor

                case "CanLineWaterfront":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(FeedbackPageLocators.CanLineWaterfrontDropdownSelector), station);
                    break;

                case "CanLineRichmond":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(FeedbackPageLocators.CanLineRichmondDropdownSelector), station);
                    break;

                case "CanLineYVR":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(FeedbackPageLocators.CanLineAirportRadioButtonDropdownSelector), station);
                    break;
            
                case "ExpLineKingGeorge":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(FeedbackPageLocators.ExpLineKGDropdownSelector), station);
                    break;

                case "ExpLineProWayUni":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(FeedbackPageLocators.ExpLinePWURadioDropdownSelector), station);
                    break;

                case "ExpLineWaterfront":
                    jse.ExecuteScript("var select = arguments[0]; for(var i = 0; i < select.options.length; i++){ if(select.options[i].text == arguments[1]){ select.options[i].selected = true; } }", driver.FindElement(FeedbackPageLocators.ExpLineWaterfrontDropdownSelector), station);
                    break;

                default:
                    throw new System.ArgumentException("Parameter must be a valid Skytrain Line Direction with Corresponding Station", "Skytrain Line Direction Station Type");
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void EnterRouteNumber(string routeNumber)
        {
            driver.FindElement(FeedbackPageLocators.RouteNumberField).SendKeys(routeNumber); 
        }

        public void EnterFirstName(string nameType, string type)
        {
            if (type == "Bus")
            {
                if (nameType == "random")
                {
                    driver.FindElement(FeedbackPageLocators.BusFirstNameField).SendKeys(RandomWordGenerator(8, nameType)); 
                    return; 
                }

                else
                {
                    driver.FindElement(FeedbackPageLocators.BusFirstNameField).SendKeys(RandomWordGenerator(2, nameType));
                    return; 
                }               
            }

            if (type == "SkyTrain")
            {
                if (nameType == "random")
                {
                    driver.FindElement(FeedbackPageLocators.STFirstNameField).SendKeys(RandomWordGenerator(8, nameType));
                    return;
                }

                else
                {
                    driver.FindElement(FeedbackPageLocators.STFirstNameField).SendKeys(RandomWordGenerator(2, nameType));
                    return;
                }
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }

        public void EnterIncidentDate(string date, string type)
        {
            if (type == "Bus")
            {
                //string selectedDate = SystemDate(date); 
                driver.FindElement(FeedbackPageLocators.BusIncidentDateField).SendKeys(SystemDate(date)); //Call DateTimeGenerator 
                return; 
            }

            if (type == "SkyTrain")
            {
                driver.FindElement(FeedbackPageLocators.STIncidentDateField).SendKeys(SystemDate(date)); //Call DateTimeGenerator 
                return;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }

        public void EnterIncidentTime(string time, string type)
        {
            if (type == "Bus")
            {
                driver.FindElement(FeedbackPageLocators.BusIncidentTimeField).SendKeys(SystemTime(time));
                return; 
            }

            if (type == "SkyTrain")
            {
                driver.FindElement(FeedbackPageLocators.STIncidentTimeField).SendKeys(SystemTime(time));
                return; 
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }

        public void EnterPhoneNumber(string phoneNumber, string type)
        {
            if (type == "Bus")
            {
                driver.FindElement(FeedbackPageLocators.BusPhoneNumberField).SendKeys(phoneNumber);
                return;
            }

            if (type == "SkyTrain")
            {
                driver.FindElement(FeedbackPageLocators.STPhoneNumberField).SendKeys(phoneNumber);
                return;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Bus or SkyTrain", "Feedback Type");
            }
        }
        // add skytrain
        public void EnterResponseChoice(string choice, string type)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

            if (choice == "yes" && type == "Bus")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.BusCustRepResponseYesButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice == "no" && type == "Bus")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.BusCustRepResponseNoButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice == "yes" && type == "SkyTrain")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.STCustRepResponseYesButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice == "no" && type == "SkyTrain")
            {
                jse.ExecuteScript("arguments[0].click()", driver.FindElement(FeedbackPageLocators.STCustRepResponseNoButton));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                return;
            }

            if (choice != "no" || choice != "yes" || type != "Bus" || type != "SkyTrain" )
            {
                throw new Exception("Error: Please Include Response Value of either: yes or no AND Feedback Value of either: " +
                    "Bus or Skytrain");
            }
        }

        public void HightlightText(string highLightColour)
        {
            TextHighLightJS HighLighter = new TextHighLightJS();
            HighLighter.HighlightElement(driver, driver.FindElement(FeedbackPageLocators.CustomerFeedbackTitle), highLightColour);
        }
    }

}
