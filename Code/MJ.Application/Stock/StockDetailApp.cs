using Chloe.Annotations;
using MJ.Application;
using MJ.Entity.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Application
{
    /// <summary>
    /// 更新库存明细数据处理类
    /// </summary>
    public class StockDetailApp : BaseApp<StockDetail>
    {
        public List<StockDetail> GetStockDetails()
        {
            try
            {
                string sql = "select  wareid Supplier_Sku_No,(sumqty-sumawaitqty-sumpendingqty) as Quantity from u_store_m";
                var details = DbContext.SqlQuery<StockDetail>(sql, null);
                return details;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
