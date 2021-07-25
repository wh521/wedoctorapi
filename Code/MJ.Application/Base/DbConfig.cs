using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Core.Security;

namespace MJ.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class DbConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbKey"></param>
        /// <returns></returns>
        public static string GetDbConnectionString(string dbKey)
        {
            return DES.Decrypt(ConfigurationManager.ConnectionStrings[dbKey].ConnectionString);
        }

        /// <summary>
        /// WMS连接字符串
        /// </summary>
        /// <param name="dbKeyForWMS"></param>
        /// <returns></returns>
        public static string GetDbConnectionStringForWMS(string dbKeyForWMS)
        {
            return DES.Decrypt(ConfigurationManager.ConnectionStrings[dbKeyForWMS].ConnectionString);
        }
    }
     
}
