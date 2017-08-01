﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tripeace.Domain.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime FromUnixTime(int unixTime)
        {
            if (unixTime <= 0) return DateTime.Now;

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static DateTime FromUnixTime(long unixTime)
        {
            if (unixTime <= 0) return DateTime.Now;

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static int FromUnixTimeDaysPeriod(long unixTime)
        {
            return DateTime.Now.Date.Subtract(FromUnixTime(unixTime).Date).Duration().Days + 1;
        }

        public static int ToUnixTimeInt(DateTime date)
        {
            if (date == default(DateTime)) return 0;

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt32((date - epoch).TotalSeconds);
        }

        public static long ToUnixTimeLong(DateTime date)
        {
            if (date == default(DateTime)) return 0;

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
    }
}
