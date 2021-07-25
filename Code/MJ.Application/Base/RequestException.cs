using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Application.Base
{
    /// <summary>
    /// 请求异常类
    /// </summary>
    public class RequestException:Exception
    {
        public int status = 911;
        public string message;
        public RequestException(string message)
        {
            this.message = message;
        }
    }
}
