using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClients.Services.PartyBuild.Helper
{
    public static class TimeHelper
    {
        public static DateTime UtcZero = new System.DateTime(1970, 1, 1);

        public static long DateToUTCTime(DateTime dt)
        {
            double dResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(UtcZero);
            dResult = (dt - startTime).TotalMilliseconds;
            return (long)dResult;
            /*旧*/
            //long tick = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;//- 8 * 60 * 60;
            //return GetBytes(tick);
        }
    }
}
