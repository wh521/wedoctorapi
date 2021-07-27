using MJ.ApiCore.WeDector;
using MJ.Application.Base;
using MJ.Core.Extensions;
using MJ.Entity;
using MJ.Entity.Order_Delivery;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MJ.Application
{
    public class WeDoctorRequestApp
    {
        private const long SUPPLYER_ID = 489842338181414912;
        private const long SUPPLYER_SHOPID = 622805701206474753;


        /// <summary>
        /// 2.1-库存更新
        /// </summary>
        public static JObject Post_UpdateStock()
        {
            if (string.IsNullOrEmpty(WeDectorConfiger.ApiUrl))
            {
                throw new ArgumentNullException("url未配置");
            }


            StringBuilder postJsonBody = new StringBuilder();

            postJsonBody.Append("{");
            postJsonBody.Append(string.Format("\"supplier_shop_id\":\"{0}\",", SUPPLYER_SHOPID));
            postJsonBody.Append(string.Format("\"supplier_id\":\"{0}\",", SUPPLYER_ID));

            postJsonBody.Append("\"update_list\":[");
            postJsonBody.Append("{");
            postJsonBody.Append("\"quantity\":1,");
            postJsonBody.Append(string.Format("\"supplier_sku_no\":\"{0}\"", "75351"));
            postJsonBody.Append("}");

            postJsonBody.Append("]");
            postJsonBody.Append("}");


            string jsonBodyStr = postJsonBody.ToString();


            string methodName = "guahao.express.supply.updatestock";
            string message_id = Guid.NewGuid().ToString();
            long timestamp = DateTimeExt.GetTimestamp13();
            string contentMD5Value = WeDoctorApi.ContentMd5(postJsonBody.ToString());
            string sign = WeDoctorApi.Sign(postJsonBody.ToString(), methodName, timestamp.ToString(), message_id);

            HttpContent httpContent = new StringContent(postJsonBody.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(WeDectorConfiger.ApiHeader.ContentType);

            httpContent.Headers.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
            httpContent.Headers.Add("method", methodName);
            httpContent.Headers.Add("timestamp", timestamp.ToString());
            httpContent.Headers.Add("version", WeDectorConfiger.ApiHeader.Version);
            httpContent.Headers.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
            httpContent.Headers.Add("message-id", message_id);
            httpContent.Headers.Add("sign", sign);
            httpContent.Headers.Add("content-md5", contentMD5Value);


            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(WeDectorConfiger.ApiUrl, httpContent).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }


        /// <summary>
        /// 3.1-获取订单列表
        /// </summary>
        /// <param name="url">Api访问地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns></returns>
        public static JObject Post_SendOrderList()
        {
            if (string.IsNullOrEmpty(WeDectorConfiger.ApiUrl))
            {
                throw new ArgumentNullException("url未配置");
            }


            StringBuilder postJsonBody = new StringBuilder();

            postJsonBody.Append("{");

            postJsonBody.Append(string.Format("\"supplier_shop_id\":\"{0}\",", SUPPLYER_SHOPID));
            postJsonBody.Append("\"offset\":0,");
            postJsonBody.Append(string.Format("\"start\":\"{0}\",", "2021-01-01"));
            postJsonBody.Append("\"send_status\":40,");
            postJsonBody.Append(string.Format("\"end\":\"{0}\",", "2021-10-01"));
            postJsonBody.Append("\"page_no\":0,");
            postJsonBody.Append(string.Format("\"supplier_id\":\"{0}\",", SUPPLYER_ID));
            postJsonBody.Append("\"page_size\":0");
            postJsonBody.Append("}");

            string jsonBodyStr = postJsonBody.ToString();

            string methodName = "guahao.express.supply.sendorderlist";
            string message_id = Guid.NewGuid().ToString();
            long timestamp = DateTimeExt.GetTimestamp13();
            string contentMD5Value = WeDoctorApi.ContentMd5(postJsonBody.ToString());
            string sign = WeDoctorApi.Sign(postJsonBody.ToString(), methodName, timestamp.ToString(), message_id);

            HttpContent httpContent = new StringContent(postJsonBody.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(WeDectorConfiger.ApiHeader.ContentType);

            httpContent.Headers.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
            httpContent.Headers.Add("method", methodName);
            httpContent.Headers.Add("timestamp", timestamp.ToString());
            httpContent.Headers.Add("version", WeDectorConfiger.ApiHeader.Version);
            httpContent.Headers.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
            httpContent.Headers.Add("message-id", message_id);
            httpContent.Headers.Add("sign", sign);
            httpContent.Headers.Add("content-md5", contentMD5Value);


            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(WeDectorConfiger.ApiUrl, httpContent).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }


        /// <summary>
        /// 3.2-获取订单详情
        /// </summary>
        /// <param name="url">Api访问地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns></returns>
        public static JObject Post_SendOrderDetail(long orderId= 626480859469316096)
        {
            if (string.IsNullOrEmpty(WeDectorConfiger.ApiUrl))
            {
                throw new ArgumentNullException("url未配置");
            }

            //请求报文
            StringBuilder postJsonBody = new StringBuilder();

            postJsonBody.Append("{");

            postJsonBody.Append(string.Format("\"supplier_shop_id\":{0},", SUPPLYER_SHOPID));
            postJsonBody.Append(string.Format("\"id\":{0},", orderId));
            postJsonBody.Append(string.Format("\"supplier_id\":{0},", SUPPLYER_ID));

            postJsonBody.Append("}");

            string jsonBodyStr = postJsonBody.ToString();

            string methodName = "guahao.express.supply.sendorderdetail";
            string message_id = Guid.NewGuid().ToString();
            long timestamp = DateTimeExt.GetTimestamp13();
            string contentMD5Value = WeDoctorApi.ContentMd5(postJsonBody.ToString());
            string sign = WeDoctorApi.Sign(postJsonBody.ToString(), methodName, timestamp.ToString(), message_id);

            HttpContent httpContent = new StringContent(postJsonBody.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(WeDectorConfiger.ApiHeader.ContentType);

            httpContent.Headers.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
            httpContent.Headers.Add("method", methodName);
            httpContent.Headers.Add("timestamp", timestamp.ToString());
            httpContent.Headers.Add("version", WeDectorConfiger.ApiHeader.Version);
            httpContent.Headers.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
            httpContent.Headers.Add("message-id", message_id);
            httpContent.Headers.Add("sign", sign);
            httpContent.Headers.Add("content-md5", contentMD5Value);


            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(WeDectorConfiger.ApiUrl, httpContent).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }


        /// <summary>
        /// 3.4-标记订单异常
        /// </summary>
        /// <param name="url">Api访问地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns></returns>
        public static JObject Post_SendOrderRefuse(List<OrderRefuse> orderRefuseList)
        {
            if (string.IsNullOrEmpty(WeDectorConfiger.ApiUrl))
            {
                throw new ArgumentNullException("url未配置");
            }

            //请求报文
            StringBuilder postJsonBody = new StringBuilder();

            postJsonBody.Append("{");
            postJsonBody.Append("\"update_list\":[");

            //循环计数变量
            int k = 1;
            foreach (OrderRefuse item in orderRefuseList)
            {
                postJsonBody.Append("{");
                postJsonBody.Append(string.Format("\"supplier_shop_id\":{0},", SUPPLYER_SHOPID));
                postJsonBody.Append(string.Format("\"refuse_order_type\":\"{0}\",", item.Refuse_Order_Type));
                postJsonBody.Append(string.Format("\"id\":{0},", item.OrderId));
                postJsonBody.Append(string.Format("\"supplier_id\":{0}", SUPPLYER_ID));
                if (k==orderRefuseList.Count)
                {
                    postJsonBody.Append("}");
                }
                else
                {
                    postJsonBody.Append("},");
                }
                k++;
            }

            postJsonBody.Append("]");
            postJsonBody.Append("}");

            string jsonBodyStr = postJsonBody.ToString();

            string methodName = "guahao.express.supply.sendorderrefuse";
            string message_id = Guid.NewGuid().ToString();
            long timestamp = DateTimeExt.GetTimestamp13();
            string contentMD5Value = WeDoctorApi.ContentMd5(postJsonBody.ToString());
            string sign = WeDoctorApi.Sign(postJsonBody.ToString(), methodName, timestamp.ToString(), message_id);

            HttpContent httpContent = new StringContent(postJsonBody.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(WeDectorConfiger.ApiHeader.ContentType);

            httpContent.Headers.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
            httpContent.Headers.Add("method", methodName);
            httpContent.Headers.Add("timestamp", timestamp.ToString());
            httpContent.Headers.Add("version", WeDectorConfiger.ApiHeader.Version);
            httpContent.Headers.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
            httpContent.Headers.Add("message-id", message_id);
            httpContent.Headers.Add("sign", sign);
            httpContent.Headers.Add("content-md5", contentMD5Value);


            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(WeDectorConfiger.ApiUrl, httpContent).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }


        /// <summary>
        /// 4.4-订单发货
        /// </summary>
        /// <param name="url">Api访问地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns></returns>
        public static JObject Post_SendOrderDelivery(List<OrderDelivery> orderDeliveryList)
        {
            if (string.IsNullOrEmpty(WeDectorConfiger.ApiUrl))
            {
                throw new ArgumentNullException("url未配置");
            }

            //请求报文
            StringBuilder postJsonBody = new StringBuilder();

            postJsonBody.Append("{");
            postJsonBody.Append("\"update_list\":[");

            //循环计数变量
            int delivery_index = 1;
            foreach (OrderDelivery delivery in orderDeliveryList)
            {
                postJsonBody.Append("{");
                //主单信息
                postJsonBody.Append(string.Format("\"supplier_shop_id\":{0},", SUPPLYER_SHOPID));
                postJsonBody.Append(string.Format("\"verification_code\":\"{0}\",", delivery.Verification_Code));
                postJsonBody.Append(string.Format("\"id\":{0},", delivery.DeliveryId));
                postJsonBody.Append(string.Format("\"supplier_id\":{0},", SUPPLYER_ID));
                //物流信息
                postJsonBody.Append("\"logistics\":{");
                postJsonBody.Append(string.Format("\"order_no\":\"{0}\",", delivery.Logistics.Order_No));
                postJsonBody.Append(string.Format("\"delivery_person\":\"{0}\",", delivery.Logistics.Delivery_Person));
                postJsonBody.Append(string.Format("\"company\":\"{0}\",", delivery.Logistics.Company));
                postJsonBody.Append(string.Format("\"company_code\":\"{0}\",", delivery.Logistics.Company_Code));
                postJsonBody.Append(string.Format("\"type\":{0},", delivery.Logistics.Type));
                postJsonBody.Append(string.Format("\"status\":{0},", delivery.Logistics.Status));
                postJsonBody.Append(string.Format("\"delivery_phone\":\"{0}\"", delivery.Logistics.Delivery_Phone));
                postJsonBody.Append("},");
                //明细数据
                int detail_index = 1;   //明细数据循环计数器
                postJsonBody.Append("\"details\":[");
                foreach (OrderDeliveryDetail detail in delivery.Details)
                {
                    postJsonBody.Append("{");
                    postJsonBody.Append(string.Format("\"batch_no\":\"{0}\",", detail.Batch_No));
                    postJsonBody.Append(string.Format("\"send_quantity\":{0},", detail.Send_Quantity));
                    postJsonBody.Append(string.Format("\"production_date\":\"{0}\",", detail.Production_Date));
                    postJsonBody.Append(string.Format("\"piats_code\":\"{0}\",", detail.Piats_Code));
                    postJsonBody.Append(string.Format("\"detail_id\":{0},", detail.Detail_Id));
                    postJsonBody.Append(string.Format("\"expiration_date\":\"{0}\"", detail.Expiration_Date));
                    if (detail_index==delivery.Details.Count)
                    {
                        postJsonBody.Append("}");
                    }
                    else
                    {
                        postJsonBody.Append("},");
                    }
                    detail_index++;
                }
                postJsonBody.Append("]");
                if (delivery_index == orderDeliveryList.Count)
                {
                    postJsonBody.Append("}");
                }
                else
                {
                    postJsonBody.Append("},");
                }
                delivery_index++;
            }
            postJsonBody.Append("]");
            postJsonBody.Append("}");

            string jsonBodyStr = postJsonBody.ToString();

            string methodName = "guahao.express.supply.sendorderdelivery";
            string message_id = Guid.NewGuid().ToString();
            long timestamp = DateTimeExt.GetTimestamp13();
            string contentMD5Value = WeDoctorApi.ContentMd5(postJsonBody.ToString());
            string sign = WeDoctorApi.Sign(postJsonBody.ToString(), methodName, timestamp.ToString(), message_id);

            HttpContent httpContent = new StringContent(postJsonBody.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue(WeDectorConfiger.ApiHeader.ContentType);

            httpContent.Headers.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
            httpContent.Headers.Add("method", methodName);
            httpContent.Headers.Add("timestamp", timestamp.ToString());
            httpContent.Headers.Add("version", WeDectorConfiger.ApiHeader.Version);
            httpContent.Headers.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
            httpContent.Headers.Add("message-id", message_id);
            httpContent.Headers.Add("sign", sign);
            httpContent.Headers.Add("content-md5", contentMD5Value);


            HttpClient httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = httpClient.PostAsync(WeDectorConfiger.ApiUrl, httpContent).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }













        private static Boolean StatusCodeHandler(HttpResponseMessage response)
        {
            Boolean ct = true;
            String statusCode = response.StatusCode.ToString();
            string result = response.Content.ReadAsStringAsync().Result;
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (statusCode == HttpStatusCode.Unauthorized.ToString())
            {
                throw new Exception(string.Format("{0},{1}", statusCode, result));
            }
            else if (statusCode == HttpStatusCode.Forbidden.ToString())
            {
                throw new Exception(string.Format("{0},{1}", statusCode, result));
            }
            else if (statusCode == HttpStatusCode.NotFound.ToString())
            {
                throw new Exception(string.Format("{0},{1}", statusCode, result));
            }
            else if (statusCode == HttpStatusCode.BadRequest.ToString())
            {
                throw new Exception(string.Format("{0},{1}", statusCode, result));
            }
            else if (statusCode == HttpStatusCode.InternalServerError.ToString())
            {
                throw new Exception(string.Format("{0},{1}", statusCode, result));
            }
            return ct;
        }




        #region 请求封装

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JObject GetResponse(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw new ArgumentNullException("url");
                }
                if (url.StartsWith("https"))
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                }

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                StatusCodeHandler(response);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }

        public static T GetResponse<T>(string url)
              where T : class, new()
        {
            T result = default(T);
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    result = JsonConvert.DeserializeObject<T>(s);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        private static string getHeaderByKey(HttpResponseMessage response, string key)
        {
            string result = "";
            string header = response.Headers.ToString();
            string[] headers = header.Split("\r\n".ToCharArray());
            if (headers.Count() > 0)
            {
                foreach (string item in headers)
                {
                    Regex reg = new Regex("^" + key + ":(.+)");
                    if (reg.IsMatch(item))
                    {
                        string[] tokens = item.Split(':');
                        if (tokens[0].ToString() == key)
                        {
                            result = tokens[1].ToString();
                            break;
                        }
                    }
                    else
                    {
                        reg = new Regex("^" + key + "=(.+)");
                        if (reg.IsMatch(item))
                        {
                            string[] tokens = item.Split('=');
                            if (tokens[0].ToString() == key)
                            {
                                result = tokens[1].ToString();
                                break;
                            }
                        }
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// 发起post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url</param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static T PostResponse<T>(string url, string postData)
           where T : class, new()
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();

            T result = default(T);

            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                StatusCodeHandler(response);
                if (response.IsSuccessStatusCode)
                {
                    Task<string> t = response.Content.ReadAsStringAsync();
                    string s = t.Result;
                    result = JsonConvert.DeserializeObject<T>(s);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        public static JObject PostResponse(string url, string postData)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                StatusCodeHandler(response);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }

        /// <summary>
        /// put请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="putData">put数据</param>
        /// <returns></returns>
        public static JObject PutResponse(string url, string putData = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpContent httpContent = new StringContent(putData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient httpClient = new HttpClient();

            try
            {
                HttpResponseMessage response = httpClient.PutAsync(url, httpContent).Result;
                StatusCodeHandler(response);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }

        /// <summary>
        /// delete请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JObject DeleteResponse(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (url.StartsWith("https"))
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = httpClient.DeleteAsync(url).Result;
                StatusCodeHandler(response);
                string statusCode = response.StatusCode.ToString();
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                    return jo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return new JObject();
        }

        #endregion
    }
}


