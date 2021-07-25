using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public static class AttributeUtil
    {
        /// <summary>
        /// 获取实体类指定特性
        ///  示例：DescriptionAttribute aa = GetPropertyCustomAttribute<Product>(t=>t.ProductCD,typeof(DescriptionAttribute)) as DescriptionAttribute;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static A GetPropertyCustomAttribute<A,T>(Expression<Func<T, object>> expression) where A :Attribute
        {
            PropertyInfo pi = PropertyUtil.GetProperty<T>(expression);
            Type attrType = typeof(A);
            A firstAttr = pi.GetCustomAttributes(attrType, false).FirstOrDefault() as A;
            return firstAttr;

        }


        /// <summary>
        /// 获取实体类Description值 示例：string desc = GetPropertyDescriptionAttributeValue<T>(t=>t.ProductCD);
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyDescriptionAttributeValue<T>(Expression<Func<T, object>> expression)
        {
            string descAttributeValue = "";
            PropertyInfo pi = PropertyUtil.GetProperty<T>(expression);
            DescriptionAttribute descAttribute = pi.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            if (descAttribute != null)
            {
                descAttributeValue = descAttribute.Description;
            }
            return descAttributeValue;
        }

        /// <summary>
        ///  获取实体类Description值 示例: GetPropertyDescriptionAttributeValue<T>("ProductID")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string GetPropertyDescriptionAttributeValue<T>(string propName)
        {
            string descAttributeValue = "";
            PropertyInfo pi = PropertyUtil.GetProperty<T>(propName);
            DescriptionAttribute descAttribute = pi.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            if (descAttribute != null)
            {
                descAttributeValue = descAttribute.Description;
            }
            return descAttributeValue;
        }
    }
}
