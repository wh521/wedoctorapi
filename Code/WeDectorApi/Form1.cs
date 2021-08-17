using MJ.ApiCore.WeDector;
using MJ.Application;
using MJ.Core.Extensions;
using MJ.Core.Security;
using MJ.Entity;
using MJ.Entity.Order_Delivery;
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
            MessageBox.Show(Convert.ToDecimal(1045 / 100.00).ToString());
            MessageBox.Show(Math.Ceiling(Convert.ToDecimal(1045.00 / 100.00)).ToString());
            return;

            WeDoctorRequestApp _wedoctorApp = new WeDoctorRequestApp();
            _wedoctorApp.Post_UpdateStock();
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


            //查询订单
            //JObject joOrder = WeDoctorRequestApp.Post_SendOrderList();
            //获取订单明细
            //JObject joOrderDetial = WeDoctorRequestApp.Post_SendOrderDetail();
            //标识订单异常
            //List<OrderRefuse> orderRefuseList = new List<OrderRefuse>();
            //orderRefuseList.Add(new OrderRefuse(1, "测试拒绝订单001"));
            //orderRefuseList.Add(new OrderRefuse(2, "测试拒绝订单002"));
            //orderRefuseList.Add(new OrderRefuse(3, "测试拒绝订单003"));
            //JObject joResult = WeDoctorRequestApp.Post_SendOrderRefuse(orderRefuseList);


            //发货接口
            List<OrderDelivery> orderDeliveryList = new List<OrderDelivery>();


            OrderDelivery delivery_1 = new OrderDelivery();

            delivery_1.DeliveryId = 626480859469316096;
            delivery_1.Verification_Code = string.Empty;

            OrderDeliveryLogistics logistics_1 = new OrderDeliveryLogistics();
            logistics_1.Order_No = "测试物流单号001";
            logistics_1.Delivery_Person = "骑手";
            logistics_1.Company = "顺丰速递";
            logistics_1.Company_Code = "测试物流单号001";
            logistics_1.Type = 1;
            logistics_1.Status = 30;
            logistics_1.Delivery_Phone = "13222225555";
            delivery_1.Logistics = logistics_1;

            delivery_1.Details = new List<OrderDeliveryDetail>();
            OrderDeliveryDetail detail_1 = new OrderDeliveryDetail();
            detail_1.Batch_No = "B00001";
            detail_1.Send_Quantity = 1;
            detail_1.Production_Date = "20210101";
            detail_1.Piats_Code = string.Empty;
            detail_1.Detail_Id = 10000001;
            detail_1.Expiration_Date = "20221230";
            delivery_1.Details.Add(detail_1);

            OrderDeliveryDetail detail_2 = new OrderDeliveryDetail();
            detail_2.Batch_No = "B00002";
            detail_2.Send_Quantity = 1;
            detail_2.Production_Date = "20210101";
            detail_2.Piats_Code = string.Empty;
            detail_2.Detail_Id = 10000001;
            detail_2.Expiration_Date = "20221230";
            delivery_1.Details.Add(detail_2);

            orderDeliveryList.Add(delivery_1);




            OrderDelivery delivery_2 = new OrderDelivery();
            delivery_2.DeliveryId = 626480859469316096;
            delivery_2.Verification_Code = string.Empty;

            OrderDeliveryLogistics logistics_2 = new OrderDeliveryLogistics();
            logistics_2.Order_No = "测试物流单号002";
            logistics_2.Delivery_Person = "骑手02";
            logistics_2.Company = "顺丰速递";
            logistics_2.Company_Code = "测试物流单号002";
            logistics_2.Type = 1;
            logistics_2.Status = 30;
            logistics_2.Delivery_Phone = "13222225555";
            delivery_2.Logistics = logistics_2;

            delivery_2.Details = new List<OrderDeliveryDetail>();
            OrderDeliveryDetail detail_3 = new OrderDeliveryDetail();
            detail_3.Batch_No = "B00003";
            detail_3.Send_Quantity = 1;
            detail_3.Production_Date = "20210101";
            detail_3.Piats_Code = string.Empty;
            detail_3.Detail_Id = 10000001;
            detail_3.Expiration_Date = "20221230";
            delivery_2.Details.Add(detail_3);

            OrderDeliveryDetail detail_4 = new OrderDeliveryDetail();
            detail_4.Batch_No = "B00004";
            detail_4.Send_Quantity = 1;
            detail_4.Production_Date = "20210101";
            detail_4.Piats_Code = string.Empty;
            detail_4.Detail_Id = 10000001;
            detail_4.Expiration_Date = "20221230";
            delivery_2.Details.Add(detail_4);

            orderDeliveryList.Add(delivery_2);


            //JObject joDevliyer = new WeDoctorRequestApp().Post_SendOrderDelivery(orderDeliveryList);


            return;
        }
    }
}
