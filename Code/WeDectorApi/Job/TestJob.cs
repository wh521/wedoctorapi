using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDectorApi.Job
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                HellWorld();
            }
            catch (NotImplementedException ex)
            {
                throw ex;
            }
        }

        private void HellWorld()
        {
            string str = "111111";
        }
    }
}
