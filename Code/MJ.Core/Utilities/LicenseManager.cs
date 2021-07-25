using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Core.Security;
using MJ.Core.Extensions;

namespace MJ.Core.Utilities
{
    /// <summary>
    /// 法拉鼎授权认证码
    /// </summary>
    public class LicenseManager
    {
        private const string LicenseKey = "8F7FA001-FC1C-4191-83FE-270514370EFF";
        private const string SecurityKey = "#Zkhz2020-FaradyneWMS";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="error_message"></param>
        /// <returns></returns>
        public static bool CheckLicense(out string error_message)
        {
            error_message = "";
            return CheckLicense(LicenseKey,out error_message);
        }



        /// <summary>
        /// 校验位(2).授权码MD5(8F7FA001-FC1C-4191-83FE-270514370EFF).认证方式(1:次数 2:期间 9:无期限).次数/期间(10/开始时间戳-结束时间戳)
        /// --->DES('#Zkhz2020-FaradyneWMS')
        /// </summary>
        /// <param name="licenseKey"></param>
        /// <param name="error_message"></param>
        /// <returns></returns>
        public static bool CheckLicense(string licenseKey,out string error_message)
        {
            string licenseText = Configuration.AppConfigHelper.ReadSetting("Lic");
            error_message = "";

            try
            {
                string _reallicenseText = DES.Decrypt(licenseText, SecurityKey);
                string[] _licenseFullInfo = _reallicenseText.Split('.');
                string _checkBit = _licenseFullInfo[0];
                string _licenseKey = _licenseFullInfo[1];
                string _licenseType = _licenseFullInfo[2];
                string _licenseInfo = _licenseFullInfo[3];

                if (!IsValidCheckBit(_checkBit, _licenseKey, int.Parse(_licenseType)))
                {
                    error_message = "不合法的授权码！";
                    return false;
                }
                
                if(!_licenseKey.Equals(MD5.MD5Encrypt(MD5.MD5Encrypt(licenseKey))))
                {
                    error_message = "授权码不正确！";
                    return false;
                }

                if ("2".Equals(_licenseType))
                {
                    DateTime dt = DateTime.Now;
                    DateTime dtStart = int.Parse(_licenseInfo.Split('-')[0]).ConvertToDateTime();
                    DateTime dtEnd = int.Parse(_licenseInfo.Split('-')[1]).ConvertToDateTime();
                    if(!dt.IsBetween(dtStart,dtEnd))
                    {
                        error_message = "授权已过期，请续费！";
                        return false;
                    }
                }

                return true;

            }
            catch(Exception ex)
            {
                error_message = "不合法的授权码！";
                return false;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseKey"></param>
        /// <param name="count"></param>
        /// <returns></returns>

        public static string GenerateCountLicense(string licenseKey, int count)
        {
            string _checkBit = GetCheckBit(licenseKey, 1);
            string _licenseKey = MD5.MD5Encrypt(MD5.MD5Encrypt(licenseKey));
            string _licenseType = "1";
            string _licenseInfo = count.ToString();
            return DES.Encrypt(_checkBit + "." + _licenseKey + "." + _licenseType + "." + _licenseInfo, SecurityKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseKey"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static string GeneratePeriodLicense(string licenseKey, DateTime startTime,DateTime endTime)
        {
            string _checkBit = GetCheckBit(licenseKey, 2);
            string _licenseKey = MD5.MD5Encrypt(MD5.MD5Encrypt(licenseKey));
            string _licenseType = "2";
            string _licenseInfo = startTime.GetTimestamp10() + "-"+endTime.GetTimestamp10();
            return DES.Encrypt(_checkBit + "." + _licenseKey + "." + _licenseType + "." + _licenseInfo,SecurityKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseKey"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static string GenerateNoLimitLicense(string licenseKey, int Count)
        {
            string _checkBit = GetCheckBit(licenseKey, 1);
            string _licenseKey = MD5.MD5Encrypt(MD5.MD5Encrypt(licenseKey));
            string _licenseType = "9";
            string _licenseInfo = DateTime.Now.ToString("yyyyMMddHHmmss");
            return DES.Encrypt(_checkBit + "." + _licenseKey + "." + _licenseType + "." + _licenseInfo,SecurityKey);
        }


        #region 私有辅助方法

        /// <summary>
        /// 获取校验位
        /// </summary>
        /// <param name="licenseKey"></param>
        /// <param name="licenseType"></param>
        /// <returns></returns>

        private static string GetCheckBit(string licenseKey, int licenseType)
        {
            string md5Char = MD5.MD5Encrypt(MD5.MD5Encrypt(licenseKey));
            int iResult = 0;
            foreach (char c in md5Char)
            {
                //int asciiValue = ;
                iResult += (int)c;
            }

            iResult += licenseType;

            return iResult.ToString().Substring(0, 2);
        }

        private static bool IsValidCheckBit(string checkBit,string licenseKey, int licenseType)
        {
            try
            {
                int iResult = 0;
                foreach (char c in licenseKey)
                {
                    //int asciiValue = ;
                    iResult += (int)c;
                }

                iResult += licenseType;
                string _checkBit = iResult.ToString().Substring(0, 2);

                return _checkBit.Equals(checkBit);
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        #endregion
    }
}
