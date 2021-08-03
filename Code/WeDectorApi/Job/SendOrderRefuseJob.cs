using MJ.Application;
using MJ.Entity;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    class SendOrderRefuseJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Execute_SendOrderRefuse();
        }

        public void Execute_SendOrderRefuse()
        {
            new WeDoctorRequestApp().Post_SendOrderRefuse();
        }
    }
}
