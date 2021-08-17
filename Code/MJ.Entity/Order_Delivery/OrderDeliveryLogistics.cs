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
    [Table("Order_Delivery_Logistics")]
    public class OrderDeliveryLogistics:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey = true)]
        public string DataId { get; set; }
        /// <summary>
        /// 发货主单ID
        /// </summary>
        [Description("发货主单ID")]
        public long Send_Id { get; set; }
        /// <summary>
        /// 物流单号，当物流类型：1-快递-必填，2-线下配送-非必填、4-自提物流-非必填
        /// </summary>
        [Description("物流单号")]
        public string Order_No { get; set; }
        /// <summary>
        /// 骑手姓名，物流类型：2-线下配送-必填
        /// </summary>
        [Description("骑手姓名")]
        public string Delivery_Person { get; set; }
        /// <summary>
        /// 必填字段
        /// 物流公司
        /// </summary>
        [Description("物流公司")]
        public string Company { get; set; }
        /// <summary>
        /// 参考Api官网物流公司编码；物流类型：1-快递-必填，2-线下配送-非必填，4.自提物流-非必填
        /// </summary>
        [Description("物流公司编码")]
        public string Company_Code { get; set; }
        /// <summary>
        /// 必填字段
        /// 物流类型：1-快递，2-线下配送，4.自提物流
        /// </summary>
        [Description("物流类型")]
        public int Type { get; set; }
        /// <summary>
        /// 物流状态：30-已发货
        /// </summary>
        [Description("物流状态")]
        public int Status { get; set; }
        /// <summary>
        /// 骑手电话,物流类型:2-线下配送-必填
        /// </summary>
        [Description("骑手电话")]
        public string Delivery_Phone { get; set; }

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
