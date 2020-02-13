using System;


namespace TranslinkSite.HelperFunctions
{
    public class DateTimeGenerator
    {
        public static string SystemDate(string date)
        {
            string returnDate;
            string dateFormat = "yyyy/MM/dd";

            if (date == "today")
            {
                DateTime today = DateTime.Today;
                returnDate = today.ToString(dateFormat);
                return returnDate;
            }

            if (date == "today-7days")
            {
                returnDate = DateTime.Today.AddDays(-7).ToString(dateFormat);
                return returnDate;
            }

            if (date == "today+10days")
            {
                returnDate = DateTime.Today.AddDays(+10).ToString(dateFormat);
                return returnDate;
            }

            else
            {
                throw new Exception("Error: Please Include Today Type Value of either: today or today-7days or today+10days ");
            }
        }        
    }
}
