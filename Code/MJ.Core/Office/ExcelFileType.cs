using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Office
{
    /// <summary>
    /// 
    /// </summary>
    public enum ExcelFileType
    {
        /// <summary>
        /// 2007以下版本(限制行数65536)
        /// </summary>
        xls = 1,

        /// <summary>
        /// Excel 2007及以后版本(限制行数：1048576 限制列数:16384)
        /// </summary>
        xlsx = 2
    }
}
