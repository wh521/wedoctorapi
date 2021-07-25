using log4net;
using log4net.Core;

namespace MJ.Core.Logging.log4netEx
{
    /// <summary>
    /// 扩展log4net
    /// </summary>
    public static class Log4NetExtension
    {
        /// <summary>
        /// 表示操作日志的Level
        /// </summary>
        public static readonly Level EventLevel = new Level(10001, "EVENT");

        public static void Event(this ILog log, string message)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, EventLevel, message, null);
        }

        public static void EventFormat(this ILog log, string format, params object[] args)
        {
            string formattedMessage = string.Format(format, args);
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, EventLevel, formattedMessage, null);
        }
    }
}
