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
    [Table("OrderDelivery")]
    public class OrderDelivery:BaseEntity
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        [Description("供应商Id")]
        public long Supplier_Id { get; set; }
        /// <summary>
        /// 供应商店铺Id
        /// </summary>
        [Description("供应商店铺Id")]
        public long Supplier_Shop_Id { get; set; }
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
