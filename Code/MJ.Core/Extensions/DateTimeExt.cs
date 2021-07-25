using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Extensions
{
    /// <summary>
    /// DateTime类型扩展
    /// </summary>
    public static class DateTimeExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetTimestamp10(this DateTime dt)
        {
            var timeSpan = dt - new DateTime(1970, 1, 1, 0, 0, 0);
            return (int)timeSpan.TotalSeconds;
        }

        public static long GetTimestamp13()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }

        /// <summary>
        /// 位于指定时间区间内
        /// </summary>
        /// <param name="dtm"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static bool IsBetween(this DateTime dtm,DateTime startTime,DateTime endTime)
        {
            if (dtm == null) return false;
            return dtm >= startTime && dtm <= endTime;
        }

    }
}
