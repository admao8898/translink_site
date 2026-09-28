using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace TranslinkSite.HelperFunctions
{
    public class GeolocationPermissionGranter
    {
        public void GrantGeolocationPermission(
            IWebDriver driver,
            string targetUrl)
        {
            if (driver is not ChromeDriver chromeDriver)
            {
                Console.WriteLine(
                    "Geolocation permission setup skipped. " +
                    "Browser is not Chrome.");

                return;
            }

            var siteUri = new Uri(targetUrl);
            var origin = siteUri.GetLeftPart(UriPartial.Authority);

            chromeDriver.ExecuteCdpCommand(
                "Browser.grantPermissions",
                new Dictionary<string, object>
                {
                    ["origin"] = origin,
                    ["permissions"] = new[] { "geolocation" }
                });

            Console.WriteLine(
                $"Geolocation permission granted for: {origin}");
        }

        public void SetSimulatedLocation(
            IWebDriver driver,
            double latitude,
            double longitude,
            double accuracy = 10)
        {
            if (driver is not ChromeDriver chromeDriver)
            {
                Console.WriteLine(
                    "Simulated location setup skipped. " +
                    "Browser is not Chrome.");

                return;
            }

            chromeDriver.ExecuteCdpCommand(
                "Emulation.setGeolocationOverride",
                new Dictionary<string, object>
                {
                    ["latitude"] = latitude,
                    ["longitude"] = longitude,
                    ["accuracy"] = accuracy
                });

            Console.WriteLine(
                $"Simulated location: {latitude}, {longitude} " +
                $"(accuracy: {accuracy}m)");
        }
    }
}