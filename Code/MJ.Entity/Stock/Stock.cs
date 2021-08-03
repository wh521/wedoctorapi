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
    [Table("Stock")]
    public class Stock:BaseEntity
    {
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
        /// 库存明细列表
        /// </summary>
        [Description("库存明细列表")]
        public List<StockDetail> StockList { get; set; }
    }
}
