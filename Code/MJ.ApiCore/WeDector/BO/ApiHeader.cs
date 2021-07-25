using System;
using System.Collections.Generic;
using System.Text;

namespace MJ.ApiCore.WeDector
{
    /// <summary>
    /// Api请求头属性信息
    /// </summary>
    public class ApiHeader
    {
        /// <summary>
        /// 用户秘钥管理中的AccessKey ID
        /// </summary>
        public string AppKey { get; set; }
        
        public string AppSecret { get; set; }
        /// <summary>
        /// 接口版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 接口请求响应类型
        /// </summary>
        public string ContentType { get; set; }
    }
}
