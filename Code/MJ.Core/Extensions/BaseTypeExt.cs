using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Extensions
{
    public static class BaseTypeExt
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this int timestamp)
        {
            return new DateTime(1970, 1, 1, 8, 0, 0).AddSeconds(timestamp);//  第四个参数可认为是时区，中国在东8区
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceStr"></param>
        /// <param name="separatorStr"></param>
        /// <returns></returns>
        public static int GetFrequencyOfOccurrence(this string sourceStr,char separatorStr)
        {
            //source.Split()
            //string[] aa = sourceStr.Split("abc");
            return sourceStr.Split(separatorStr).Length-1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theType"></param>
        /// <returns></returns>
        public static Type GetRealType(this Type theType)
        {
            if (theType.IsNullableType())
            {
                return Nullable.GetUnderlyingType(theType);
            }
            return theType;
        }

    }
}
