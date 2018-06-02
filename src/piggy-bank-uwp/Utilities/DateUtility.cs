using System;

namespace piggy_bank_uwp.Utilities
{
    public static class DateUtility
    {
        private static readonly DateTime UTCStartDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long GetUTCMillisecondsFromDateTime(DateTime date)
        {
            DateTime utcDateTime = date.ToUniversalTime();
            return (long)utcDateTime.Subtract(UTCStartDate).TotalMilliseconds;
        }

        public static DateTime GetLocalTimeFromUTCMilliseconds(long utcMelliseconds)
        {
            return UTCStartDate.AddMilliseconds(utcMelliseconds).ToLocalTime();
        }

        public static DateTime GetUniversalFromUTCMilliseconds(long utcMelliseconds)
        {
            return UTCStartDate.AddMilliseconds(utcMelliseconds).ToUniversalTime();
        }
    }
}
