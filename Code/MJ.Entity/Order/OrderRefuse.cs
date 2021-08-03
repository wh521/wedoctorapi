using Chloe.Annotations;
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
    [Table("Order_Refuse")]
    public class OrderRefuse: BaseEntity
    {
        public OrderRefuse() { }

        public OrderRefuse(long OrderId, string Refuse_Order_Type)
        {
            this.OrderId = OrderId;
            this.Refuse_Order_Type = Refuse_Order_Type;
        }
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey =true)]
        public string DataId { get; set; }
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
    }
}
