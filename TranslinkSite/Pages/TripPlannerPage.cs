using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using TranslinkSite.HelperFunctions;
using static TranslinkSite.HelperFunctions.ExcelToDataTableConverter;

namespace TranslinkSite.Pages
{
    public class TripPlannerPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly string TripPlannerURL = "https://new.translink.ca/trip-planner";

        private static readonly By TripPlannerTextLink = By.XPath("//*[text()='Trip Planner']");

        public readonly string tripPlannerPageDescription = "Tell us where you're starting from and where you want to go and we'll find the best route to get you there. Use Google Trip Planner or Try our ";
        public readonly string tripPlannerPageDescriptFailMsg = "Incorrect Trip Planner Description";

        //Google Map Text 
        private static readonly By GMapStartPoint = By.XPath("//*[@aria-label='Starting point Simon Fraser University']");
        private static readonly By GMapEndPoint = By.XPath("//*[@aria-label='Destination The University of British Columbia']");
        
        private static readonly By FromTextBox = By.Id("tripplannerwidget-prevpoint");
        private static readonly By ToTextBox = By.Id("tripplannerwidget-nextpoint");
        private static readonly By ChangeDirectionButton = By.XPath("//*[@class='changeDirectionButton']");
        private static readonly By PlanMyTripButton = By.Id("planMyTrip");
        private static readonly By MoreOptionsLink = By.XPath("(//*[@class='Header'])[2]");

        private static readonly By PreferedTransitOptionDropdownSelector = By.Name("tripPreferences");
        private static readonly By RouteDropdownSelector = By.Id("tripplannerwidget-routepreferences");
        // ** Time and Departing Option hidden on mobile view 

        public TripPlannerPage(IWebDriver drv)
        {
            driver = drv;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void GoToTripPlannerLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(TripPlannerTextLink));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        public void GoToTripPlannerURL()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(TripPlannerURL);
        }

        public void EnterFromDestinationText(string startingPoint)
        {
            driver.FindElement(FromTextBox).SendKeys(startingPoint); 
        }

        public void EnterToDestinationText(string endPoint)
        {
            driver.FindElement(ToTextBox).SendKeys(endPoint);
        }

        public void ClickPlanMyTripButton()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(PlanMyTripButton));
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        //Verify Google Map Destinations match Trip Planner Destinations 
        public void VerifyGoogleMaps()
        {
            Boolean isPresentStart = driver.FindElement(GMapStartPoint).Displayed;
            Boolean isPresentEnd = driver.FindElement(GMapEndPoint).Displayed; 

            if (isPresentStart == false || isPresentEnd == false)
            {
                throw new Exception("Error: Start or End Destination are not correct"); 
            }

            else
            {
                return; 
            }           
        }

        public void ClickMoreOptionsLink()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", driver.FindElement(MoreOptionsLink));
            //driver.FindElement(MoreOptionsLink).Click(); 
        }

        public void VerifyAllDropdownOptions(string type)
        {
            string[] dropList;
            IWebElement dropdownChoice;

            switch (type)
            {
                case "Prefer":
                    dropList = new string[] { "", "B", "S", "T" };
                    dropdownChoice = driver.FindElement(PreferedTransitOptionDropdownSelector);
                    break;

                case "Routes":
                    var dropList11 = ImportSheet("test_file.xlsx");

                    string[] arrayList = dropList11.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                    dropList = arrayList; 

                    //dropList =  new string[] {"def", "num", "walk", "whe" };
                    dropdownChoice = driver.FindElement(RouteDropdownSelector);
                    break;
               
                default:
                    throw new System.ArgumentException(("Error: Please Include Dropdown Type Value of either: Prefer or Routes"));
            }

            SelectElement dropDownptions = new SelectElement(dropdownChoice);
            IList<IWebElement> options = dropDownptions.Options;
            int numberofOptions = options.Count;

            IWebElement dropDownActualValue;
            // using a for loop to match all option values against desired values 
            // reference to https://stackoverflow.com/questions/9562853/how-to-get-all-options-in-a-drop-down-list-by-selenium-webdriver-using-c

            for (int i = 1; i < numberofOptions; i++)
            {
                dropDownActualValue = options[i];
                Assert.AreEqual(dropDownActualValue.GetAttribute("value"), dropList[i], "One or more of the dropdown options are missing or incorrect");
            }
        }

        public void SelectPreferedTransitMode(string option)
        {
            new SelectElement(driver.FindElement(PreferedTransitOptionDropdownSelector)).SelectByText(option);
        }

        public void SelectPreferedRouteMode(string option)
        {
            new SelectElement(driver.FindElement(RouteDropdownSelector)).SelectByText(option);
        }

        public void ExcelConverter()
        {
            var dropList = ImportSheet("test_file.xlsx");

            string[] arrayList  = dropList.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();

            //foreach (DataRow row in dropList.Rows)
            //{
            //    Console.WriteLine();
            //    for (int x = 0; x < dropList.Columns.Count; x++)
            //    {
            //        Console.Write(row[x].ToString() + " ");
            //    }
            //}
        }
    }
}
