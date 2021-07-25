using MJ.Core.Logging.log4netEx;
using log4net;
using log4net.Core;

namespace MJ.Core.Logging
{
    /// <summary>
    /// 静态单例，线程安全
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 系统日志器
        /// </summary>
        private ILog _sysLogger { get; set; }
        /// <summary>
        /// 业务日志器
        /// </summary>
        private ILog _eventLogger { get; set; }

        static LogHelper()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        private LogHelper()
        {
            //添加扩展的log4net level
            if (!LogManager.GetRepository().LevelMap.AllLevels.Contains(Log4NetExtension.EventLevel))
            {
                LogManager.GetRepository().LevelMap.AllLevels.Add(Log4NetExtension.EventLevel);
            }

            _sysLogger = LogManager.GetLogger("SysLogger");
            _eventLogger = LogManager.GetLogger("EventLogger");
        }

        /// <summary>
        /// 静态单例，线程安全
        /// </summary>
        public static LogHelper Instance { get; } = new LogHelper();

        private void addCustomLevel(ILog log, Level level)
        {
            if (!log.Logger.Repository.LevelMap.AllLevels.Contains(level))
            {
                log.Logger.Repository.LevelMap.Add(level);
            }
        }

        #region 系统日志方法
        /// <summary>
        /// 记录系统日志，INFO
        /// </summary>
        /// <param name="message"></param>
        public void LogInfo(string message)
        {
            _sysLogger.Info(message);
        }
        /// <summary>
        /// 记录系统日志，INFO，格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogInfoFormat(string format, params object[] args)
        {
            _sysLogger.InfoFormat(format, args);
        }
        /// <summary>
        /// 记录系统日志，DEBUG
        /// </summary>
        /// <param name="message"></param>
        public void LogDebug(string message)
        {
            _sysLogger.Debug(message);
        }
        /// <summary>
        /// 记录系统日志，DEBUG，格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogDebugFormat(string format, params object[] args)
        {
            _sysLogger.DebugFormat(format, args);
        }
        /// <summary>
        /// 记录系统日志，WARN
        /// </summary>
        /// <param name="message"></param>
        public void LogWarn(string message)
        {
            _sysLogger.Warn(message);
        }
        /// <summary>
        /// 记录系统日志，WARN，格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogWarnFormat(string format, params object[] args)
        {
            _sysLogger.WarnFormat(format, args);
        }
        /// <summary>
        /// 记录系统日志，ERROR
        /// </summary>
        /// <param name="message"></param>
        public void LogError(string message)
        {
            _sysLogger.Error(message);
        }
        /// <summary>
        /// 记录系统日志，ERROR，格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void LogErrorFormat(string format, params object[] args)
        {
            _sysLogger.ErrorFormat(format, args);
        }

        #endregion

        #region 业务日志方法
        /// <summary>
        /// 记录业务日志(登录日志或操作日志)
        /// </summary>
        /// <param name="message"></param>
        private void logEvent(string message)
        {
            _eventLogger.Event(message);
        }

        /// <summary>
        /// 记录业务日志(登录日志或操作日志)，格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        private void logEventFormat(string format, params object[] args)
        {
            _eventLogger.EventFormat(format, args);
        }

        #region 由日志实体生成日志
        /// <summary>
        /// 记录业务日志(登录日志或操作日志)
        /// </summary>
        /// <param name="logTmp"></param>
        public void LogEvent(BaseLogTemplate logTmp)
        {
            var tmpType = logTmp.GetType();
            var tmpProperties = tmpType.GetProperties();
            if (tmpProperties.Length > 0)
            {
                foreach (var pi in tmpProperties)
                {
                    log4net.LogicalThreadContext.Properties[pi.Name] = pi.GetValue(logTmp);
                }
            }
            this.logEvent(logTmp.LogContent);
        }
        #endregion

        #endregion
    }
}
