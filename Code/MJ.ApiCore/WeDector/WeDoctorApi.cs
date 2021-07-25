using MJ.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MJ.ApiCore.WeDector
{
    public static class WeDoctorApi
    {
        /// <summary>
        /// 生成Api请求签名
        /// </summary>
        /// <param name="jsonBody">请求Json格式的报文</param>
        /// <returns></returns>
        public static string ContentMd5(string jsonBody)
        {
            try
            {
                /*
                    1、将 公共参数 + HTTP Query 参数（即，URL后面拼接的参数或FORM表单字段）按照字母先后顺序排序得到：keyvaluekeyvalue...keyvalue即得到拼接字符串originalSignStr。
                    公共参数仅包括：appkey、method、timestamp、version、product-code、message-id、content-md5（该字段仅当Content-type为application/json时才会参与签名，且Content-type字段不参与签名）
                    
                    1-A:
                    JSON报文数据（若JSON报文为空，则content-md5不参与加签构造）
	                a. 首先对JSON请求体数据（JSON整体，若需要加密则为加密后的JSON报文）进行md5并转化为32位大写字符串
	                b. 放置于请求头content-md5字段的value值中
	                c. 以content-md5作为键参与键值对排序
        
                    如： 请求头为 method="guahao.hospdept.search"，appkey="123456"
    	                 JSON原始报文为 jsonBody={"name":"test","age":25,"department":"门诊"，"hospital":"同济医院"}
                    首先：将jsonBody进行md5并转化为32位大写字符串获得contentMd5Value：BEC73AAA277077898BCEA487365782FE
                    其次：将contentMd5Value置于请求头content-md5字段的value值中：content-md5="BEC73AAA277077898BCEA487365782FE"
    	                此时请求头为：method="guahao.hospdept.search"，appkey="123456"，content-md5="BEC73AAA277077898BCEA487365782FE"
                 */
                //待加签字符串
                string contentMd5Value = string.Empty;
                if (!string.IsNullOrEmpty(jsonBody))
                {
                    contentMd5Value = MD5.MD5Encrypt(jsonBody).ToUpper();
                }
                return contentMd5Value;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("生成Api请求签名发生异常，{0}", ex.Message));
            }
        }

        /// <summary>
        /// 生成Api请求签名
        /// </summary>
        /// <param name="jsonBody">请求Json格式的报文</param>
        /// <returns></returns>
        public static string Sign(string jsonBody,string methodName,string timestamp,string messageId)
        {
            try
            {
                /*
                    1、将 公共参数 + HTTP Query 参数（即，URL后面拼接的参数或FORM表单字段）按照字母先后顺序排序得到：keyvaluekeyvalue...keyvalue即得到拼接字符串originalSignStr。
                    公共参数仅包括：appkey、method、timestamp、version、product-code、message-id、content-md5（该字段仅当Content-type为application/json时才会参与签名，且Content-type字段不参与签名）
                    
                    1-A:
                    JSON报文数据（若JSON报文为空，则content-md5不参与加签构造）
	                a. 首先对JSON请求体数据（JSON整体，若需要加密则为加密后的JSON报文）进行md5并转化为32位大写字符串
	                b. 放置于请求头content-md5字段的value值中
	                c. 以content-md5作为键参与键值对排序
        
                    如： 请求头为 method="guahao.hospdept.search"，appkey="123456"
    	                 JSON原始报文为 jsonBody={"name":"test","age":25,"department":"门诊"，"hospital":"同济医院"}
                    首先：将jsonBody进行md5并转化为32位大写字符串获得contentMd5Value：BEC73AAA277077898BCEA487365782FE
                    其次：将contentMd5Value置于请求头content-md5字段的value值中：content-md5="BEC73AAA277077898BCEA487365782FE"
    	                此时请求头为：method="guahao.hospdept.search"，appkey="123456"，content-md5="BEC73AAA277077898BCEA487365782FE"
                    最后：以content-md5作为键参与键值对排序拼接，得到字符串源：
    	                originalSignStr = appkey123456content-md5BEC73AAA277077898BCEA487365782FEmethodguahao.hospdept.search
                 */
                //处理Header参数，按键排序
                Dictionary<string, string> dictHeader = new Dictionary<string, string>();
                dictHeader.Add("appkey", WeDectorConfiger.ApiHeader.AppKey);
                dictHeader.Add("method", methodName);
                dictHeader.Add("timestamp", timestamp);
                dictHeader.Add("version", WeDectorConfiger.ApiHeader.Version);
                dictHeader.Add("product-code", WeDectorConfiger.ApiHeader.ProductCode);
                dictHeader.Add("message-id", messageId);
                dictHeader.Add("content-md5", ContentMd5(jsonBody));

                //以键排序
                Dictionary<string, string> dictOrderBy = dictHeader.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                //待加签字符串
                StringBuilder targetSignStr = new StringBuilder();
                targetSignStr.Append("appsecret");
                foreach (string key in dictOrderBy.Keys)
                {
                    if (!string.IsNullOrEmpty(jsonBody))
                    {
                        targetSignStr.Append(key);
                        targetSignStr.Append(dictOrderBy[key]);
                    }
                    else
                    {
                        if (key!= "content-md5")
                        {
                            targetSignStr.Append(key);
                            targetSignStr.Append(dictOrderBy[key]);
                        }
                    }
                }
                targetSignStr.Append(WeDectorConfiger.ApiHeader.AppSecret);

                //if (!string.IsNullOrEmpty(jsonBody))
                //{
                //    targetSignStr = string.Format("appsecretappkey{0}content-md5{1}method{2}timestamp{3}version{4}product-code{5}message-id{6}{7}",
                //                                WeDectorConfiger.ApiHeader.AppKey,
                //                                ContentMd5(jsonBody),
                //                                methodName,
                //                                timestamp,
                //                                WeDectorConfiger.ApiHeader.Version,
                //                                WeDectorConfiger.ApiHeader.ProductCode,
                //                                messageId,
                //                                WeDectorConfiger.ApiHeader.AppSecret);
                //}
                //else
                //{
                //    targetSignStr = string.Format("appsecretappkey{0}method{1}timestamp{2}version{3}product-code{4}message-id{5}{6}",
                //                                WeDectorConfiger.ApiHeader.AppKey,
                //                                methodName,
                //                                timestamp,
                //                                WeDectorConfiger.ApiHeader.Version,
                //                                WeDectorConfiger.ApiHeader.ProductCode,
                //                                messageId,
                //                                WeDectorConfiger.ApiHeader.AppSecret);
                //}
                //生成签名
                string targetStr = targetSignStr.ToString();
                string signStr = MD5.MD5Encrypt(targetSignStr.ToString()).ToUpper();
                return signStr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("生成Api请求签名发生异常，{0}", ex.Message));
            }
        }
    }
}
