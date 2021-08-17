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
            WeDoctorRequestApp _req = new WeDoctorRequestApp();
            _req.Post_SendOrderList();
            //_req.Post_SendOrderDetail();
        }
    }
}
