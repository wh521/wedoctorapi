using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Entity
{
    /// <summary>
    /// 实体比较泛型类(实体约束，必须含有主键)
    /// </summary>
    public class EntityComparer<T> : IEqualityComparer<T> where T : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return x.GetPrimaryKeyValue() == y.GetPrimaryKeyValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
            //return obj.GetPrimaryKeyValue().GetHashCode();
        }
    }
}
