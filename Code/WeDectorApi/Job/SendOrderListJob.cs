using MJ.Application;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    class SendOrderListJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Execute_SendOrderList();
        }

        public void Execute_SendOrderList()
        {
            WeDoctorRequestApp.Post_SendOrderList();
            WeDoctorRequestApp.Post_SendOrderDetail();
        }
    }
}
