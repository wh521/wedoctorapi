using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Office
{
    /// <summary>
    /// 自定义导出特性
    /// </summary>
    public class ExcelExportAttribute:Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="desc"></param>
        public ExcelExportAttribute(string desc)
        {
            this.Description = desc;
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 默认宽度
        /// </summary>
        public int DefaultWidth { get; set; }

        /// <summary>
        /// 数据库字段长度(可显示字符)
        /// </summary>
        public int DbLength { get; set; }

        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool IsIgnore { get; set; } = false;

        /// <summary>
        /// 数据格式
        /// </summary>
        public ExcelCellDataFormat DataFormat { get; set; }

        /// <summary>
        /// 是否默认隐藏
        /// </summary>
        public bool Hidden { get; set; }
    }

    
}
