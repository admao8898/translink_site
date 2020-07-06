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
using static TranslinkSite.HelperFunctions.DropdownListVerifier;

namespace TranslinkSite.Pages
{
    public class TripPlannerPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly string tripPlannerURL = "https://new.translink.ca/trip-planner";

        private static readonly By TripPlannerTextLink = By.XPath("//*[text()='Trip Planner']");

        public readonly string tripPlannerPageDescription = "Tell us where you're starting from and where you want to go " +
            "and we'll find the best route to get you there. Use Google Trip Planner or Try our ";
        public readonly string tripPlannerPageDescriptFailMsg = "Incorrect Trip Planner Description";

        //Google Map Text 
        private static readonly By GMapStartPoint = By.XPath("//*[@aria-label='Starting point Simon Fraser University']");
        private static readonly By GMapEndPoint = By.XPath("//*[@aria-label='Destination The University of British Columbia']");
        
        private static readonly By FromTextBox = By.Id("prev_point_desktop");
        private static readonly By ToTextBox = By.Id("next_point_desktop");
        private static readonly By ChangeDirectionButton = By.XPath("//*[@class='changeDirectionButton']");
        private static readonly By PlanMyTripButton = By.XPath("//*[text()='Plan my trip']");
        private static readonly By MoreOptionsLink = By.XPath("//*[text()='More options']");

        private static readonly By PreferedTransitOptionDropdownSelector = By.Name("tripPreferences");
        private static readonly By RouteOptionDropdownSelector = By.Name("routePreferences");
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
            driver.Navigate().GoToUrl(tripPlannerURL);
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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        //Verify Google Map Destinations match Trip Planner Destinations 
        //Fails when run on the cloud (Azure DevOps), but works on local machine
        //Out of scope for tests, but will leave for further investigation
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
        }

        public void VerifyAllDropdownOptions(string type)
        {
            string[] dropList;
            IWebElement dropdownChoice;
            DropdownListVerifier DropdownListVerify = new DropdownListVerifier();

            switch (type)
            {
                case "Prefer":
                    dropList = new string[] { "", "B", "T", "S" };
                    dropdownChoice = driver.FindElement(PreferedTransitOptionDropdownSelector);
                    break;

                case "Routes":
                    // String data taken from Excel spreadsheet and converted into a datatable 
                    // Which is then converted to an array 
                    var dropListRoutes = ImportSheet("TripPlannerRouteDropdownValues.xlsx");

                    dropList = dropListRoutes.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                    dropdownChoice = driver.FindElement(RouteOptionDropdownSelector);
                    break;
               
                default:
                    throw new System.ArgumentException("Parameter must either be Prefer or Routes", "Dropdown Type Value");
            }

            DropdownListVerify.VerifiyDropdownValues(driver, dropdownChoice, dropList);
        }

        public void SelectPreferedTransitMode(string option)
        {
            new SelectElement(driver.FindElement(PreferedTransitOptionDropdownSelector)).SelectByText(option);
        }

        public void SelectPreferedRouteMode(string option)
        {
            new SelectElement(driver.FindElement(RouteOptionDropdownSelector)).SelectByText(option);
        }
        
        //Displays values in datatable via Console 
        public void ViewValuesInDataTable()
        {
            var dropList = ImportSheet("test_file.xlsx");
            foreach (DataRow row in dropList.Rows)
            {
                Console.WriteLine();
                for (int x = 0; x < dropList.Columns.Count; x++)
                {
                    Console.Write(row[x].ToString() + " ");
                }
            }
        }

        public void ClickChangeDirectionButton()
        {
            driver.FindElement(ChangeDirectionButton).Click(); 
        }

    }
}
