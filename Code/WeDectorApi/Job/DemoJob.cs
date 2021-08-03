using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    public class DemoJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            DemoJobMethod();
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task DemoJobMethod()
        {
            string str = "00000";
        }
    }
}
