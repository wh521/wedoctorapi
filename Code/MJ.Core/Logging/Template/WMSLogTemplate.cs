using System;

namespace MJ.Core.Logging
{
    /// <summary>
    /// 业务日志模板
    /// </summary>
    public class WMSLogTemplate : BaseLogTemplate
    {
        /// <summary>
        /// 日志触发人Id
        /// </summary>
        public string CUser { get; set; }
        /// <summary>
        /// 日志触发时间
        /// </summary>
        public DateTime CTime { get; set; }
        /// <summary>
        /// 日志类型（登录/操作）
        /// </summary>
        public string LogType { get; set; }
        /// <summary>
        /// 关联业务类型（e.g. 出入库管理/盘点计划管理...）
        /// </summary>
        public string EventType { get; set; }
    }
}
