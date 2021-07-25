using MJ.ApiCore.WeDector;
using MJ.Application;
using MJ.Core.Extensions;
using MJ.Core.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeDectorApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ApiHeader header = WeDectorConfiger.ApiHeader;

            //List<Supplyer> listSupper = WeDectorConfiger.Supplyers;

            //List<Shop> listShop = WeDectorConfiger.Shops;

            //List<Method> listMethod = WeDectorConfiger.Methods;


            ////string jsonBody= "{"name":"test","age":25,"department":"门诊"，"hospital":"同济医院"}";

            //string jsonBody= "{\"name\":\"test\",\"age\":25,\"department\":\"门诊\"，\"hospital\":\"同济医院\"}";

            //string md5Str = MD5.MD5Encrypt(jsonBody).ToUpper();



            //string message_id = Guid.NewGuid().ToString();
            //long timestamp = DateTimeExt.GetTimestamp13();

            //string sign = WeDoctorApi.Sign(jsonBody.ToString(), "guahao.hospdept.search", timestamp.ToString(), message_id);





            //string timeSpan = DateTimeExt.GetTimestamp13().ToString();

            //StringBuilder postData = new StringBuilder();
            //postData.Append("\"data-raw\":{");
            //postData.Append("\"req_data\":{");

            //postData.Append(string.Format("\"supplier_shop_id\":\"{0}\",","123456"));
            //postData.Append("\"offset\":0,");
            //postData.Append("\"start\":null,");
            //postData.Append("\"send_status\":0,");
            //postData.Append("\"end\":null,");
            //postData.Append("\"page_no\":0,");
            //postData.Append(string.Format("\"supplier_id\":\"{0}\",","456789"));
            //postData.Append("\"page_size\":0 }");

            //postData.Append("}");

            //string str = postData.ToString();





            //Dictionary<string, string> dictHeader = new Dictionary<string, string>();
            //dictHeader.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
            //dictHeader.Add("method", "methodName");
            //dictHeader.Add("timestamp", "timestamp");
            //dictHeader.Add("version", WeDectorConfiger.ApiHeader.Version);
            //dictHeader.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
            //dictHeader.Add("message-id", "messageId");
            //dictHeader.Add("content-md5", "ContentMd5(jsonBody)");

            //Dictionary<string, string> dictOrderBy = dictHeader.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);

            //foreach (var item in dictOrderBy.Keys)
            //{
            //    MessageBox.Show(item.ToString());
            //}
            //return;



            JObject jo = WeDoctorRequestApp.Post_SendOrderList();

            return;
        }
    }
}
