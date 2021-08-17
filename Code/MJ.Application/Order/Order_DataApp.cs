using MJ.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Application.Order
{
    /// <summary>
    /// 发货主单
    /// </summary>
    public class Order_DataApp : BaseApp<Order_Data>
    {
        Order_DetailsApp _DetailsApp = new Order_DetailsApp();
        Order_Ext_InfoApp _extInfoApp = new Order_Ext_InfoApp();

        Order_Prescription_Show_DetailApp _prescriptionApp = new Order_Prescription_Show_DetailApp();
        Order_Tcm_DrugsApp _tcmDrugsApp = new Order_Tcm_DrugsApp();
        Order_DrugsApp _drugsApp = new Order_DrugsApp();

        //添加主单数据
        public bool CreateOrderInfo(OrderEntity orderEntity)
        {
            try
            {
                _DetailsApp.DbContext = this.DbContext;
                _extInfoApp.DbContext = this.DbContext;
                DbContext.Session.BeginTransaction();

                foreach (var data in orderEntity.data_list)
                {
                    //添加订单主单明细
                    _DetailsApp.Insert(data.details);
                    //添加订单扩展信息
                    _extInfoApp.Insert(data.ext_info);
                    if (data.prescription_show_detail!=null)
                    {
                        //添加处方数据
                        _prescriptionApp.Insert(data.prescription_show_detail);
                        if (data.prescription_show_detail.tcm_drugs_list != null)
                        {
                            //添加中药处方数据
                            _tcmDrugsApp.Insert(data.prescription_show_detail.tcm_drugs_list);
                        }
                        if (data.prescription_show_detail.drugs_list != null)
                        {
                            //添加西药处方数据
                            _drugsApp.Insert(data.prescription_show_detail.drugs_list);
                        }
                    }
                }
                //添加订单主单
                this.Insert(orderEntity.data_list);

                DbContext.Session.CommitTransaction();  //提交事务

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
