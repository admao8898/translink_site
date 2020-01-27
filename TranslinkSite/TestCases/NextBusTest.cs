using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TranslinkSite.Pages;

namespace TranslinkSite.TestCases
{
    public class NextBusTest : UITestFixture
    {
        //Next Bus ~ "NB"
        [TestCase("R5"), Order(1)]
        public void TextViewNB(string busroute)
        {
            NextBusPageElements NextBus = new NextBusPageElements(driver);
            NextBus.TextViewNB(busroute);
        }
    }
}
