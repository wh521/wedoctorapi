using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Request
{
    public class WedoctorApiResponseData
    {
        public string code { get; set; }
        public int success_count { get; set; }
        public int failed_count { get; set; }
        public string message { get; set; }

        public List<WedoctorApiResponseDetailData> failed_reason_list { get; set; }
    }

    public class WedoctorApiResponseDetailData
    {
        public string failed_msg { get; set; }

        public long id { get; set; }

        /// <summary>
        /// 失败原因-库存更新接口使用
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 供应商SKU编码-库存更新接口使用
        /// </summary>
        public string supplier_sku_no { get; set; }
    }
}
