using System; 

namespace TranslinkSite.HelperFunctions
{
    public class DateTimeGenerator
    {
        //Function returns System Date and Time. User may use dates/times are either past, current, or future relative to current date/time

        public static string SystemDate(string days)
        {
            string dateFormat = "yyyy/MM/dd";

            //Verify string is an integer
            if (int.TryParse(days, out int dayInteger))
            {
                dayInteger = Convert.ToInt32(days);
            }

            else
            {
                throw new Exception("String must be integer type only, i.e. '8' for +8 days or '-3' for -3 days");
            }

            string date = DateTime.Today.AddDays(dayInteger).ToString(dateFormat);
            return date;
        }

        public static string SystemTime(string times)
        {
            string timeFormat = "hh:mm tt";

            if (int.TryParse(times, out int timeInteger))
            {
                timeInteger = Convert.ToInt32(times);
            }

            else
            {
                throw new Exception("String must be integer type only, i.e. '8' for +8 hours or '-3' for -3 hours");
            }

            string time = DateTime.Now.AddHours(timeInteger).ToString(timeFormat);
            return time;
        }
    }
}
