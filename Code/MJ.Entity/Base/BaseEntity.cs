using Chloe.Annotations;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MJ.Entity
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    public class BaseEntity : ICloneable
    {
        ///// <summary>
        ///// 备注
        ///// </summary>
        //[Description("备注")]
        //public string Remark { get; set; }

        /// <summary>
        /// 是否已删除,1为true，0为false
        /// </summary>
        [Description("是否已删除")]
        public bool IsDelete { get; set; } = false;
        /// <summary>
        /// 插入记录的用户Id
        /// </summary>
        //[Description("创建用户")]
        //public string CUser { get; set; }
        /// <summary>
        /// 插入记录的服务器时间
        /// </summary>
        [Description("创建时间")]
        public DateTime? CTime { get; set; } = DateTime.Now;
        ///// <summary>
        ///// 修改记录的用户Id
        ///// </summary>
        //[Description("修改用户")]
        //public string MUser { get; set; }
        ///// <summary>
        ///// 修改记录的服务器时间
        ///// </summary>
        //[Description("修改时间")]
        //public DateTime? MTime { get; set; }
        /// <summary>
        /// 删除记录的用户Id
        /// </summary>
        [Description("删除用户")]
        public string DUser { get; set; }
        /// <summary>
        /// 删除记录的时间
        /// </summary>
        [Description("删除时间")]
        public DateTime? DTime { get; set; }

        /// <summary>
        /// 主键列默认赋值Guid
        /// 必须设置主键列
        /// </summary>
        public BaseEntity()
        {
            // WebAPI 开发模式，导致默认赋值会被覆盖
            //SetDefaultValueToPrimaryKey();
        }

        /// <summary>
        /// 设置实体类主键值
        /// </summary>

        public void SetDefaultValueToPrimaryKey()
        {
            Type objType = this.GetType();

            if (objType.GetProperties()?.SelectMany(x => x.GetCustomAttributes<ColumnAttribute>())?.Where(c => c.IsPrimaryKey).Count() == 1)
            {
                var pi = objType.GetProperties().Where(p => p.GetCustomAttributes<ColumnAttribute>().Where(c => c.IsPrimaryKey).Count() == 1).FirstOrDefault();
                pi.SetValue(this, Guid.NewGuid().ToString().ToUpper());
            }
            else
            {
                throw new Exception(string.Format("实体类[{0}]不存在主键列或者主键列数量大于1", objType.Name));
            }
        }

        /// <summary>
        /// 获取实体类主键值
        /// </summary>
        /// <returns></returns>
        public object GetPrimaryKeyValue()
        {
            Type objType = this.GetType();

            if (objType.GetProperties()?.SelectMany(x => x.GetCustomAttributes<ColumnAttribute>())?.Where(c => c.IsPrimaryKey).Count() == 1)
            {
                var pi = objType.GetProperties().Where(p => p.GetCustomAttributes<ColumnAttribute>().Where(c => c.IsPrimaryKey).Count() == 1).FirstOrDefault();
                return pi.GetValue(this);
            }
            else
            {
                throw new Exception(string.Format("实体类[{0}]不存在主键列或者主键列数量大于1", objType.Name));
            }
        }

        /// <summary>
        /// 获取主键字段名
        /// </summary>
        /// <returns></returns>
        public string GetPrimaryKeyField()
        {
            return "";
        }


        /// <summary>
        /// 深表复制
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }



    }
}
