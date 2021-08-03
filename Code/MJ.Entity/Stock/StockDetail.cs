using Chloe.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Stock
{
    /// <summary>
    /// 订单发货主表
    /// </summary>
    [Table("u_store_m")]
    public class StockDetail: BaseEntity
    {
        /// <summary>
        /// 供应商SKU编码,供应商导入药品三方编码
        /// </summary>
        [Description("供应商SKU编码，供应商导入药品三方编码")]
        public long Supplier_Sku_No { get; set; }
        /// <summary>
        /// 可用库存数量
        /// </summary>
        [Description("可用库存数量")]
        public long Quantity { get; set; }
    }
}
