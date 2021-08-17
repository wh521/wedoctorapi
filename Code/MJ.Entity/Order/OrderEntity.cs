using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity.Order
{
    /// <summary>
    /// 订单数据承载类
    /// </summary>
    public class OrderEntity
    {

        public string code { get; set; }

        public string message { get; set; }

        public long? next_offset { get; set; }

        public int? page_size { get; set; }

        public List<Order_Data> data_list { get; set; }

    }
}
