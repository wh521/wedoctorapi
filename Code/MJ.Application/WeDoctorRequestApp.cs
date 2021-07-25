using MJ.ApiCore.WeDector;
using MJ.Application.Base;
using MJ.Core.Extensions;
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
        private const string SUPPLYER_ID = "489842338181414912";
        private const string SUPPLYER_SHOPID = "622805701206474753";

        /// <summary>
        /// 获取订单列表
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

            postJsonBody.Append("\"req-data\":{");
            postJsonBody.Append(string.Format("\"supplier_shop_id\":\"{0}\",", SUPPLYER_SHOPID));
            postJsonBody.Append("\"offset\":0,");
            postJsonBody.Append("\"start\":null,");
            postJsonBody.Append("\"send_status\":10,");
            postJsonBody.Append("\"end\":null,");
            postJsonBody.Append("\"page_no\":0,");
            postJsonBody.Append(string.Format("\"supplier_id\":\"{0}\",", SUPPLYER_ID));
            postJsonBody.Append("\"page_size\":0 }");
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
    }
}


