namespace MJ.Core.Http
{
    public enum RequestStatusCode
    {
        /// <summary>
        /// 发生未处理异常
        /// </summary>
        UnHandledException = 1001,
        /// <summary>
        /// 请求处理成功
        /// </summary>
        Success = 2000,

        /// <summary>
        /// 处理失败
        /// </summary>
        Failed = 2010,
    }
}
