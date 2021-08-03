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
    /// 西药处方单药品
    /// </summary>
    [Table("OrderDrugs")]
    public class OrderDrugs:BaseEntity
    {
        /// <summary>
        /// 厂商
        /// </summary>
        [Description("厂商")]
        public string Drug_Manufacturer { get; set; }
        /// <summary>
        /// 商品名
        /// </summary>
        [Description("商品名")]
        public string Drug_Name { get; set; }
        /// <summary>
        /// 批准文号
        /// </summary>
        [Description("批准文号")]
        public string Drug_Batch_No { get; set; }
        /// <summary>
        /// 用法
        /// </summary>
        [Description("用法")]
        public string Drug_Usage { get; set; }
        /// <summary>
        /// 第三方业务编码
        /// </summary>
        [Description("第三方业务编码")]
        public string Supplier_Sku_No { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Description("数量")]
        public int? Drug_Count { get; set; }
        /// <summary>
        /// 用药天数
        /// </summary>
        [Description("用药天数")]
        public string Days { get; set; }
        /// <summary>
        /// 用量
        /// </summary>
        [Description("用量")]
        public string Drug_Dose { get; set; }
        /// <summary>
        /// 药品单位
        /// </summary>
        [Description("药品单位")]
        public string Drug_Unit { get; set; }
        /// <summary>
        /// 通用名
        /// </summary>
        [Description("通用名")]
        public string Common_Name { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [Description("规格")]
        public string Drug_Specifications { get; set; }
        /// <summary>
        /// 频次
        /// </summary>
        [Description("频次")]
        public string Frequency { get; set; }
    }
}
