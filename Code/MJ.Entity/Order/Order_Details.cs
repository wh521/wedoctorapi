using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Order
{
    /// <summary>
    /// 发货单子单
    /// </summary>
    [Table("Order_Details")]
    public class Order_Details:BaseEntity
    {
        /// <summary>
        /// 批准文号
        /// </summary>
        [Description("批准文号")]
        public string approval_no { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [Description("规格")]
        public string specification { get; set; }
        /// <summary>
        /// 生产厂家名称
        /// </summary>
        [Description("生产厂家名称")]
        public string manufacturer_name { get; set; }
        /// <summary>
        /// 采购单价
        /// </summary>
        [Description("采购单价")]
        public double? unit_price { get; set; }
        /// <summary>
        /// 子订单ID
        /// </summary>
        [Description("子订单ID")]
        public long? sub_order_id { get; set; }
        /// <summary>
        /// 售卖价格
        /// </summary>
        [Description("售卖价格")]
        public double? sale_price { get; set; }
        /// <summary>
        /// 处方结算金额
        /// </summary>
        [Description("处方结算金额")]
        public double? p_sale_price { get; set; }
        /// <summary>
        /// 药品通用名
        /// </summary>
        [Description("药品通用名")]
        public string drug_common_name { get; set; }
        /// <summary>
        /// 发货数量
        /// </summary>
        [Description("发货数量")]
        public int? send_quantity { get; set; }
        /// <summary>
        /// 第三方业务编码
        /// </summary>
        [Description("第三方业务编码")]
        public string supplier_sku_no { get; set; }
        /// <summary>
        /// 详单状态：40已确认;50已完成;60部分发货
        /// </summary>
        [Description("详单状态：40已确认;50已完成;60部分发货")]
        public int? send_status { get; set; }
        /// <summary>
        /// 发货单子单ID
        /// </summary>
        [Description("发货单子单ID")]
        public long? id { get; set; }
        /// <summary>
        /// 发货单主单ID
        /// </summary>
        [Description("发货单主单ID")]
        public long? send_order_id { get; set; }
    }
}
