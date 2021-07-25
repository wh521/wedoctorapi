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
    public enum ExcelCellDataFormat
    {
        /// <summary>
        /// 常规
        /// </summary>
        Convention = 13,

        /// <summary>
        /// 小数
        /// </summary>
        Numeric = 14,

        /// <summary>
        /// 文本
        /// </summary>
        Text = 1,
        /// <summary>
        /// 标准日期:2019-07-01
        /// </summary>
        StandardDate = 2,

        /// <summary>
        /// 标准时间：12:56:03
        /// </summary>
        StandardTime = 3,

        /// <summary>
        /// 标准时刻:2019-07-01 12:56:03
        /// </summary>
        StandardDateTime = 4,

        /// <summary>
        /// 中国日期：2019年7月23日
        /// </summary>
        ChineseDate = 5,

        /// <summary>
        /// 中国时间：12点45分32秒
        /// </summary>
        ChineseTime = 6,

        /// <summary>
        /// 中国时刻：2019年7月23日 12点45分32秒
        /// </summary>
        ChineseDateTime = 7,
        /// <summary>
        /// 货币(￥1,234.78)
        /// </summary>
        Currency = 8,
        /// <summary>
        /// 百分数 92.45%
        /// </summary>
        Percent = 9,

        /// <summary>
        /// 分数:2/9
        /// </summary>
        Fractional = 10,

        /// <summary>
        /// 科学计数法1.23E+02
        /// </summary>
        Scientific = 11,



        /// <summary>
        /// 中文大写货币(653.32=>陆佰伍拾叁元叁角贰分)
        /// </summary>
        ChineseCurrency = 12
    }
}
