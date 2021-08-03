using MJ.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Application
{
    /// <summary>
    /// 异常订单数据处理
    /// </summary>
    public class OrderRefuseApp : BaseApp<OrderRefuse>
    {
        /// <summary>
        /// 获取需同步的异常订单数据
        /// </summary>
        /// <returns></returns>
        public List<OrderRefuse> GetOrderRefuseData()
        {
            try
            {
                var listData = this.GetList((w) => w.ReadStatus == 0).ToList();
                return listData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新异常订单数据状态
        /// </summary>
        /// <param name="updateList">更新对象列表,对象中DateId必须存在有效值</param>
        /// <returns></returns>
        public bool UpdateOrderRefuseReadStatus(List<OrderRefuse> updateList)
        {
            try
            {
                foreach (var item in updateList)
                {
                    item.ReadStatus = 1;
                    item.ReadTime = DateTime.Now;
                }
                this.DbContext.Session.BeginTransaction();
                this.Update(updateList);
                this.DbContext.Session.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                if (this.DbContext.Session.IsInTransaction)
                {
                    this.DbContext.Session.RollbackTransaction();
                }
                throw ex;
            }
        }
    }
}
