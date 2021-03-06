using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Order_Delivery
{
    /// <summary>
    /// 订单发货主表
    /// </summary>
    [Table("Order_Delivery")]
    public class OrderDelivery:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey = true)]
        public string DataId { get; set; }
        /// <summary>
        /// 发货单主单ID
        /// </summary>
        [Description("发货单主单ID")]
        public long DeliveryId { get; set; }
        /// <summary>
        /// 核销码(自提必填)
        /// </summary>
        [Description("核销码(自提必填)")]
        public string Verification_Code { get; set; }

        /// <summary>
        /// 订单读取更新状态0:未读取 1:已更新
        /// </summary>
        [Description("订单读取更新状态0:未读取 1:已更新")]
        public int ReadStatus { get; set; }

        /// <summary>
        /// 数据读取更新时间
        /// </summary>
        [Description("数据读取更新时间")]
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 物流信息
        /// </summary>
        [Description("物流信息")]
        public OrderDeliveryLogistics Logistics { get; set; }
        /// <summary>
        /// 发货单明细
        /// </summary>
        [Description("发货单明细")]
        public List<OrderDeliveryDetail> Details { get; set; }
    }
}
