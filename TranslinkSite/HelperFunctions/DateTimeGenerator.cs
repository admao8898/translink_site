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
                throw new Exception("Error: Please Include Date Type Value of either: today or today-7days or today+10days");
            }
        } 
        
        public static string SystemTime(string time)
        {
            string returnTime;
            string timeFormat = "hh:mm tt";
            if (time == "Current")
            {
                DateTime currentTime = DateTime.Now;
                returnTime = currentTime.ToString(timeFormat);
                return returnTime;
            }

            if (time == "Current-3hours")
            {
                returnTime = DateTime.Now.AddHours(-3).ToString(timeFormat);
                return returnTime;
            }

            if (time == "Current+3hours")
            {
                returnTime = DateTime.Now.AddHours(3).ToString(timeFormat);
                return returnTime;
            }

            else
            {
                throw new System.ArgumentException("Parameter must either be Current or Current-3hours or Current+3hours", "Time Type");
            }
        }
    }
}
