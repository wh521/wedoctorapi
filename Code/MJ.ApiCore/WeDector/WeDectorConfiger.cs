using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace MJ.ApiCore.WeDector
{
    /// <summary>
    /// 微医云Api配置类
    /// </summary>
    public class WeDectorConfiger
    {
        /// <summary>
        /// 获取Aip请求头信息
        /// </summary>
        public static ApiHeader ApiHeader
        {
            get
            {
                ApiHeader header = new ApiHeader();

                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"//WeDoctorConfiger.xml");

                XmlNode xn = doc.SelectSingleNode("apisetings");

                XmlNodeList xmlNodeList = xn.ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    if (node.Name.ToLower() == "apiseting")
                    {
                        switch (node.Attributes["key"].Value.ToLower())
                        {
                            case "appkey":
                                header.AppKey = node.Attributes["value"].Value;
                                break;
                            case "appsecret":
                                header.AppSecret = node.Attributes["value"].Value;
                                break;
                            case "version":
                                header.Version = node.Attributes["value"].Value;
                                break;
                            case "product_code":
                                header.ProductCode = node.Attributes["value"].Value;
                                break;
                            case "content_type":
                                header.ContentType = node.Attributes["value"].Value;
                                break;
                        }
                    }
                }
                return header;
            }
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        public static List<Supplyer> Supplyers
        {
            get
            {
                List<Supplyer> listSuppyer = new List<Supplyer>();

                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"//WeDoctorConfiger.xml");

                XmlNode xn = doc.SelectSingleNode("apisetings");

                XmlNodeList xmlNodeList = xn.ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    if (node.Name.ToLower() == "suppliers")
                    {
                        foreach (XmlNode xmlNode in node.ChildNodes)
                        {
                            Supplyer supplyer = new Supplyer();
                            supplyer.SupplerId = xmlNode.Attributes["id"].Value;
                            supplyer.SupplerDisc = xmlNode.Attributes["disc"].Value;
                            listSuppyer.Add(supplyer);
                        }
                    }
                }
                return listSuppyer;
            }
        }

        /// <summary>
        /// 获取供应商门店信息
        /// </summary>
        public static List<Shop> Shops
        {
            get
            {
                List<Shop> listShop = new List<Shop>();

                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"//WeDoctorConfiger.xml");

                XmlNode xn = doc.SelectSingleNode("apisetings");

                XmlNodeList xmlNodeList = xn.ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    if (node.Name.ToLower() == "supplier_shops")
                    {
                        foreach (XmlNode xmlNode in node.ChildNodes)
                        {
                            Shop shop = new Shop();
                            shop.ShopId = xmlNode.Attributes["id"].Value;
                            shop.ShopDisc = xmlNode.Attributes["disc"].Value;
                            listShop.Add(shop);
                        }
                    }
                }
                return listShop;
            }
        }

        /// <summary>
        /// 获取Api接口函数名称
        /// </summary>
        public static List<Method> Methods
        {
            get
            {
                List<Method> listMethod = new List<Method>();

                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"//WeDoctorConfiger.xml");

                XmlNode xn = doc.SelectSingleNode("apisetings");

                XmlNodeList xmlNodeList = xn.ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    if (node.Name.ToLower() == "methods")
                    {
                        foreach (XmlNode xmlNode in node.ChildNodes)
                        {
                            Method method = new Method();
                            method.MethodId = xmlNode.Attributes["id"].Value;
                            method.MethodName = xmlNode.Attributes["method"].Value;
                            method.MethodDisc = xmlNode.Attributes["disc"].Value;
                            method.RequestMethod = xmlNode.Attributes["request_method"].Value;
                            listMethod.Add(method);
                        }
                    }
                }
                return listMethod;
            }
        }

        /// <summary>
        /// 获取Aip请求地址url
        /// </summary>
        public static string ApiUrl
        {
            get
            {
                string api_url = string.Empty;

                XmlDocument doc = new XmlDocument();
                doc.Load(AppDomain.CurrentDomain.BaseDirectory + @"//WeDoctorConfiger.xml");

                XmlNode xn = doc.SelectSingleNode("apisetings");

                XmlNodeList xmlNodeList = xn.ChildNodes;
                foreach (XmlNode node in xmlNodeList)
                {
                    if (node.Name.ToLower() == "apiseting")
                    {
                        if(node.Attributes["key"].Value.ToLower()== "api_url")
                        {
                            api_url = node.Attributes["value"].Value;
                        }
                    }
                }
                return api_url;
            }
        }
    }
}
