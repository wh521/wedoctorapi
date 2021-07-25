using System;
using MJ.Core.Extensions;
using MJ.Core.Security;

namespace MJ.Core.Security
{
    /// <summary>
    /// 
    /// </summary>
    public static class TokenUtil
    {
        //默认有效期设置为一天(调试方便设置为1年有效期)
        private const int _defaultExpireSeconds = 60 * 60 * 24 * 365;

        //签名默认密钥
        private const string _defaultPrivateKey = "#ZhongKeHuaZhi2019";


        #region 获取用户ID

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUid(string token)
        {

            //string[] cont = token.Split('.')[0];
            if (IsValidateToken(token))
            {
                string[] tokenPart = token.Split('.');
                var info = Base64.Decode(tokenPart[0]);
                return info.Split(',')[0].Split('=')[1].Trim();
            }
            else throw new Exception("Token无效");
            
        }

        #endregion

        #region 获取Token创建时间

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        public static DateTime GetCreateTime(string token)
        {
            if (IsValidateToken(token))
            {
                string[] tokenPart = token.Split('.');
                var info = Base64.Decode(tokenPart[0]);
                string ctime= info.Split(',')[1].Split('=')[1].Replace("{","").Trim();
                return int.Parse(ctime).ConvertToDateTime();
            }
            else throw new Exception("Token无效");
        }

        #endregion

        #region 获取Token失效时间
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static DateTime GetExpireTime(string token)
        {
            if (IsValidateToken(token))
            {
                string[] tokenPart = token.Split('.');
                var info = Base64.Decode(tokenPart[0]);
                string ctime = info.Split(',')[2].Split('=')[1].Replace("}", "").Trim();
                return int.Parse(ctime).ConvertToDateTime();
            }
            else
            {
                throw new Exception("Token无效");
            }
        }

        #endregion

        #region 获取签名

        public static string GetSign(string token)
        {
            if (IsValidateToken(token))
            {
                string[] tokenPart = token.Split('.');
                var info = Base64.Decode(tokenPart[1]);
                return info.Replace("{","").Replace("}","");
            }
            else throw new Exception("Token无效");

        }

        #endregion

        #region 验证签名
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckSign(string token)
        {
            if (!IsValidateToken(token))
            {
                return false;
            }

            //
            string uid = GetUid(token);
            string signStr = GetSign(token);
            if(!signStr.Equals(GenerateSign(uid)))
            {
                return false;
            }
            return true;

        }


        #endregion

        #region 是否有效Token
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsValidateToken(string token)
        {
            // 如果含有多个'.'无效
            if (token.GetFrequencyOfOccurrence('.')!=1)
            {
                return false;
            }
            string[] tokenPart = token.Split('.');
            var info = Base64.Decode(tokenPart[0]);
            if (!info.Contains("Uid") && !info.Contains("CTime") && !info.Contains("Exp"))
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 生成签名

        private static string GenerateSign(string uid)
        {
            return MD5.MD5Encrypt(uid+_defaultPrivateKey,32);
        }

        #endregion

        //public int ExpireSeconds
        //{
        //    get{ return _defaultExpireSeconds;}
        //    set{_defaultExpireSeconds = value;}
        //}

        //private string PrivateKey
        //{
        //    get { return _defaultPrivateKey; }
        //    set { _defaultPrivateKey = value; }
        //}

    }
}