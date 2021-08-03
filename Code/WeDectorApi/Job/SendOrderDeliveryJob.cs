using MJ.Application;
using MJ.Entity.Order_Delivery;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    class SendOrderDeliveryJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            SendOrderDelivery();
        }

        void SendOrderDelivery()
        {
            List<OrderDelivery> orderDeliveryList = new List<OrderDelivery>();
            WeDoctorRequestApp.Post_SendOrderDelivery(orderDeliveryList);
        }
    }
}
