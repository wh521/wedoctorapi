using System;
using System.Collections.Generic;
using System.Text;

namespace MJ.ApiCore.WeDector
{
    public class Method
    {
        /// <summary>
        /// Api接口方法Id
        /// </summary>
        public string MethodId { get; set; }
        /// <summary>
        /// Api接口方法名称
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// Api接口方法描述
        /// </summary>
        public string MethodDisc { get; set; }
        /// <summary>
        /// Api接口请求方式 Post/Get
        /// </summary>
        public string RequestMethod { get; set; }
    }
}
