using MJ.Entity.Order_Delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Application.Order_Delivery
{
    /// <summary>
    /// 发货主单
    /// </summary>
    public class OrderDeliveryApp : BaseApp<OrderDelivery>
    {
        OrderDeliveryDetailsApp _detailApp = new OrderDeliveryDetailsApp();
        OrderDeliveryLogisticsApp _logisticsApp = new OrderDeliveryLogisticsApp();

        /// <summary>
        /// 查询需同步的发货信息
        /// </summary>
        /// <returns></returns>
        public List<OrderDelivery> GetSyncDeliveryData()
        {
            try
            {
                var delveryList = this.GetList((w) => w.ReadStatus == 0).ToList();
                foreach (var data in delveryList)
                {
                    //物流信息
                    var logisticsData = this._logisticsApp.GetList((w) => w.Send_Id == data.DeliveryId).FirstOrDefault();
                    data.Logistics = logisticsData;
                    //发货子单列表
                    var detailList = this._detailApp.GetList((w) => w.Id == data.DeliveryId).ToList();
                    data.Details = detailList;
                }
                return delveryList;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        /// <summary>
        /// 更新发货数据读取状态
        /// </summary>
        /// <param name="updateList">更新发货数据列表</param>
        /// <returns></returns>
        public bool UpdateOrderDeliveryReadStatu(List<OrderDelivery> updateList)
        {
            try
            {
                foreach (var data in updateList)
                {
                    data.ReadStatus = 1;
                    data.ReadTime = DateTime.Now;
                    data.Logistics.ReadStatus = 1;
                    data.Logistics.ReadTime = DateTime.Now;
                    foreach (var detail in data.Details)
                    {
                        detail.ReadStatus = 1;
                        detail.ReadTime = DateTime.Now;
                    }

                    //持久化更新数据
                    _detailApp.DbContext = DbContext;
                    _logisticsApp.DbContext = DbContext;
                    DbContext.Session.BeginTransaction();

                    Update(updateList); //更新发货主单状态
                    foreach (var updateData in updateList)
                    {
                        _detailApp.Update(updateData.Details);  //更新发货明细单
                        _logisticsApp.Update(updateData.Logistics); //更新发货物流数据
                    }

                    DbContext.Session.CommitTransaction();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (DbContext.Session.IsInTransaction)
                {
                    DbContext.Session.RollbackTransaction();
                }
                throw ex;
            }
        }
    }
}
