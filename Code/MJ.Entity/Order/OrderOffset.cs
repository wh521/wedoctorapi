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
    /// 订单偏移量表
    /// </summary>
    [Table("Order_Offset")]
    public class OrderOffset:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey =true)]
        public string DataId { get; set; }
        /// <summary>
        /// 获取订单列表偏移量
        /// </summary>
        [Description("获取订单列表偏移量")]
        public long Offset { get; set; }
    }
}
