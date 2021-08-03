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
    /// 中药处方单药品
    /// </summary>
    [Table("中药处方单药品")]
    public class Order_Tcm_Drugs
    {
        /// <summary>
        /// 商品名
        /// </summary>
        [Description("商品名")]
        public string Drug_Name { get; set; }
        /// <summary>
        /// 剂量单位
        /// </summary>
        [Description("剂量单位")]
        public string Dose_Unit { get; set; }
        /// <summary>
        /// 特殊用法
        /// </summary>
        [Description("特殊用法")]
        public string Special_Usage { get; set; }
        /// <summary>
        /// 第三方业务编码
        /// </summary>
        [Description("第三方业务编码")]
        public string Supplier_Sku_No { get; set; }
        /// <summary>
        /// 药品重量,单位克
        /// </summary>
        [Description("药品重量-单位克")]
        public string Drug_Count { get; set; }
        /// <summary>
        /// 通用名
        /// </summary>
        [Description("通用名")]
        public string Common_Name { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        [Description("药品规格")]
        public string Drug_Specifications { get; set; }
       
    }
}
