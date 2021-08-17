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
    /// 订单发货明细数据类
    /// </summary>
    [Table("Order_Delivery_Detail")]
    public class OrderDeliveryDetail:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey = true)]
        public string DataId { get; set; }
        /// <summary>
        /// 发货主单ID，主外键关系
        /// </summary>
        [Description("发货主单ID，主外键关系")]
        public long Id { get; set; }
        /// <summary>
        /// 订单子单ID-发货主单ID，主外键关系
        /// </summary>
        [Description("订单子单ID-发货主单ID，主外键关系")]
        public long Detail_Id { get; set; }
        /// <summary>
        /// 产品批号
        /// </summary>
        [Description("产品批号")]
        public string Batch_No { get; set; }
        /// <summary>
        /// 发货数量
        /// </summary>
        [Description("发货数量")]
        public int Send_Quantity { get; set; }
        /// <summary>
        /// 生产日期，格式：yyyyMMdd
        /// </summary>
        [Description("生产日期，格式：yyyyMMdd")]
        public string Production_Date { get; set; }
        /// <summary>
        /// 电子监管码，多个用","号分隔
        /// </summary>
        [Description("电子监管码，多个用英文逗号号分隔")]
        public string Piats_Code { get; set; }
        /// <summary>
        /// 有效期,格式：yyyyMMdd
        /// </summary>
        [Description("有效期,格式：yyyyMMdd")]
        public string Expiration_Date { get; set; }


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
