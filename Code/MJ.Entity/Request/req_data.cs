using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Request
{
    /// <summary>
    /// 请求参数类
    /// </summary>
    public class req_data
    {
        /// <summary>
        /// 供应商店铺id
        /// </summary>
        public long supplier_shop_id { get; set; }
        /// <summary>
        /// 偏移量,用于下次查询
        /// </summary>
        public long offset { get; set; }
        /// <summary>
        /// 发货单创建时间，格式：yyyy-MM-dd,从0点开始计算
        /// </summary>
        public string start { get; set; }
        /// <summary>
        /// 发货单状态，状态（0.待确认;10.确认超时; 20.已拒 单;30.已 取消;40.已 确认;50.已 完成;60.部 分 发货）
        /// </summary>
        public int send_status { get; set; }
        /// <summary>
        /// 发货单创建时间，格式：yyyy-MM-dd，从0点开始计算，不能与start相同
        /// </summary>
        public string end { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int page_no { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        public long supplier_id { get; set; }
        /// <summary>
        /// 每页显示条数，默认20
        /// </summary>
        public int page_size { get; set; }
    }
}
