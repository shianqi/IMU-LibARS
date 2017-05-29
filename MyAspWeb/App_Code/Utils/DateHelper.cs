using System;

namespace MyAspWeb
{
    public class DateHelper
    {
        private static DateTime javaScriptTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

        public static DateTime ToDateTime(double date)
        {
            return javaScriptTime.AddMilliseconds(date);
        }

        public static double ToLong(DateTime date)
        {
            return (double)(date - javaScriptTime).TotalMilliseconds;
        }
    }
}