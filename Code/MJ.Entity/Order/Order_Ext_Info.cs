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
    }
}
