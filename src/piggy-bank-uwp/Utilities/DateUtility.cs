using System;

namespace piggy_bank_uwp.Utilities
{
    public static class DateUtility
    {
        public static long GetTimeUtc(DateTimeOffset date)
        {
            return date.ToFileTime();
        }

        public static DateTimeOffset GetDateTime(long timeUtc)
        {
            if (timeUtc == 0)
                return DateTimeOffset.Now;

            return DateTimeOffset.FromFileTime(timeUtc);
        }
    }
}
