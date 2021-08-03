using Chloe.Annotations;
using MJ.Application;
using MJ.Entity.Stock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chloe.SqlServer;
using Chloe;

namespace MJ.Application
{
    /// <summary>
    /// 更新库存数据处理类
    /// </summary>
    public class StockApp : BaseApp<Stock>
    {
        StockDetailApp _stockDetailApp = new StockDetailApp();

        /// <summary>
        /// 获取需更新的库存数据
        /// </summary>
        /// <returns></returns>
        public Stock GetUpdateStock()
        {
            Stock stock = new Stock();
            try
            {
                //查询库存明细
                var detailData = _stockDetailApp.GetStockDetails();
                if (detailData.Count > 0)
                {
                    stock.StockList = detailData;
                }
                return stock;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
