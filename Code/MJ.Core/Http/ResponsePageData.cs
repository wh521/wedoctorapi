using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Http
{
    /// <summary>
    /// 用途待定（暂未启用）
    /// </summary>
    public class ResponsePageData
    {
        /// <summary>
        /// 总数据行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 分页列表数据项
        /// </summary>
        public object items { get; set; }
        /// <summary>
        /// 扩展返回内容
        /// </summary>
        public object otherData { get; set; }
    }
}
