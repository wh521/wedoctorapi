namespace MJ.Core.Logging
{
    /// <summary>
    /// 日志模板实体基类
    /// </summary>
    public class BaseLogTemplate
    {
        ///// <summary>
        ///// 主键，保留字段
        ///// </summary>
        //public string LogTemplateId { get; set; }
        ///// <summary>
        ///// 日志格式
        ///// </summary>
        //public string LogFormat { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogContent { get; set; }
    }
}
