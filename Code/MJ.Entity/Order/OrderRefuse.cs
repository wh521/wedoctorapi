using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity
{
    /// <summary>
    /// 异常订单类
    /// </summary>
    public class OrderRefuse: BaseEntity
    {

        public OrderRefuse(long OrderId, string Refuse_Order_Type)
        {
            this.OrderId = OrderId;
            this.Refuse_Order_Type = Refuse_Order_Type;
        }
        public OrderRefuse(long Supplier_Id,long Supplier_Shop_Id, long OrderId, string Refuse_Order_Type)
        {
            this.Supplier_Id = Supplier_Id;
            this.Supplier_Shop_Id = Supplier_Shop_Id;
            this.OrderId = OrderId;
            this.Refuse_Order_Type = Refuse_Order_Type;
        }

        /// <summary>
        /// 供应商ID
        /// </summary>
        [Description("供应商ID")]
        public long Supplier_Id { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        [Description("门店ID")]
        public long Supplier_Shop_Id { get; set; }
        /// <summary>
        /// 订单主单ID
        /// </summary>
        [Description("订单主单ID")]
        public long OrderId { get; set; }
        /// <summary>
        /// 拒单原因
        /// </summary>
        [Description("拒单原因")]
        public string Refuse_Order_Type { get; set; }
    }
}
