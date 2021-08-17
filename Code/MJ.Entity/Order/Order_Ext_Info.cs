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
    /// 扩展信息
    /// </summary>
    [Table("Order_Ext_Info")]
    public class Order_Ext_Info:BaseEntity
    {
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        [Column(IsPrimaryKey = true)]
        public string DataId { get; set; }
        /// <summary>
        /// 数据记录ID
        /// </summary>
        [Description("数据记录ID")]
        public long? OrderId { get; set; }
        /// <summary>
        /// 取药窗口号
        /// </summary>
        [Description("取药窗口号")]
        public int? dispensing_window_number { get; set; }
        /// <summary>
        /// 医生签名图片
        /// </summary>
        [Description("医生签名图片")]
        public string signature_base64 { get; set; }
        /// <summary>
        /// 费别
        /// </summary>
        [Description("费别")]
        public string patient_medicare_type_name { get; set; }
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
