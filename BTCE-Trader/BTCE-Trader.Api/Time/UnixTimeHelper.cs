using System;

namespace BTCE_Trader.Api.Time
{
    public class UnixTimeHelper
    {
        public static DateTime UnixTimeToDateTime(UInt32 unixTime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
            return dtDateTime;
        }

        public static int GetCurrentUnixTime()
        {
            return (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static int GetCurrentUnixTimeForDate(DateTime date)
        {
            return (int)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
