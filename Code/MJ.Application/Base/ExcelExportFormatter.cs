using System;
using System.Collections.Generic;
using System.Linq;


namespace MJ.Application
{
    /// <summary>
    /// 全局导出数据格式化器
    /// </summary>
    public static class ExcelExportFormatter
    {
        /// <summary>
        /// 性别格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object GenderFormatter(object value,object Item)
        {
            return 1.Equals(value) ? "男" : "女";
        }

        /// <summary>
        /// 性别格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object IsConsignFormatter(object value, object Item)
        {
            return "1".Equals(value) ? "寄售标签" : "非寄售标签";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object IsEnableFormatter(object value, object Item)
        {
            return true.Equals(value) ? "可用" : "冻结";
        }

        /// <summary>
        /// 是/否 格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object IsOrNoFormatter(object value, object Item)
        {
            return 1.Equals(value) ? "是" : "否";
        }

        /// <summary>
        /// 是/否 格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object ExamineOrNoFormatter(object value, object Item)
        {
            return 1.Equals(value) ? "已审核" : "未审核";
        }
        /// <summary>
        /// 是/否 格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object IStatusFormatter(object value, object Item)
        {
            return 1.Equals(value) ? "合格" : "不合格";
        }

        /// <summary>
        /// 时间格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object DateTimeFormatter(object value, object Item)
        {
            return DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 时间格式化器
        /// </summary>
        /// <param name="value"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static object DateTimeFormatter2(object value, object Item)
        {
            return DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd ");
        }
    }
}
