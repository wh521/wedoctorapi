using MJ.Application;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    class UpdateStockJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Execute_UpdateStock();
        }

        protected void Execute_UpdateStock()
        {
            new WeDoctorRequestApp().Post_UpdateStock();
        }
    }
}
