using System;

namespace MJ.Core.Http
{
    /// <summary>
    /// 服务返回数据格式化类
    /// </summary>
    [Serializable]
    public class ResponseData
    {
        /// <summary>
        /// 返回状态码：自定义
        /// </summary>
        public int Code { get; set; } = 2000;

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 返回消息参量（多语言）
        /// </summary>
        public object MessageParam { get; set; } = null;

        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; } = null;
    }
}
