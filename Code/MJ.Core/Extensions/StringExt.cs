using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MJ.Core.Extensions
{
    /// <summary>
    /// String相关扩展
    /// </summary>
    public static class StringExt
    {
        #region 获取字符串中的英文字母

        /// <summary>
        /// 提取字符串中的英文字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ExtractLetterPart(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in str)
            {
                Regex reg1 = new Regex(@"^[A-Za-z]+$");
                if (reg1.IsMatch(ch.ToString()))
                {
                    sb.Append(ch);

                }
            }
            return sb.ToString();

        }

        #endregion

        #region 为空或者包含

        /// <summary>
        /// 字符串为空或者包含特定字符
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static bool IsEmptyOrContains(this string source,string keyword)
        {
            return string.IsNullOrEmpty(source) || source.Contains(keyword);
        }

        #endregion

        #region 数组元素是否含有相同项目(不考虑顺序)

        /// <summary>
        /// 数组元素是否含有相同项目
        /// </summary>
        /// <param name="arrSource"></param>
        /// <param name="arrDestination"></param>
        /// <returns></returns>

        public static bool HaveSameElements(this string[] arrSource, string[] arrDestination)
        {
            var q = from a in arrSource join b in arrDestination on a equals b select a;
            bool flag = arrSource.Length == arrDestination.Length && q.Count() == arrSource.Length;
            return flag;//内容相同返回true,反之返回false。
        }


        #endregion


        /// <summary>
        /// 字符串转数值类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ToInt(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            else
                return Convert.ToInt32(s);
        }

        /// <summary>
        /// 日期转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            else
                return Convert.ToDateTime(s);
        }

    }
}
