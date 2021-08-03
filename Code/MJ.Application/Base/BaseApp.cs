using Chloe;
using MJ.Entity;
using MJ.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using MJ.Core.Utilities;
using MJ.Application;

namespace MJ.Application
{

    /// <summary>
    /// Ado.net 并不支持并行事务，所以批量操作都单独重载一个带有事务参数的方法
    /// 外层添加事务的情况下，事务内处理时请不要使用带有Tran结尾的方法
    ///  where T : BaseEntity, new()
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseApp<T> : ContentBase where T : BaseEntity, new()
    {


        #region 添加(修正完毕)

        #region  单个实体添加

        /// <summary>
        /// 添加单实体，返回成功插入的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Insert(T entity)
        {
            if (string.IsNullOrEmpty(entity.GetPrimaryKeyValue()?.ToString()))
            {
                entity.SetDefaultValueToPrimaryKey();
            }

            //获取实体自定义特性字段
            Type objType = entity.GetType();
            //取属性上的自定义特性
            foreach (PropertyInfo propInfo in objType.GetProperties())
            {
                object[] objAttrs = propInfo.GetCustomAttributes(typeof(UniqueCodeAttribute), true);
                if (objAttrs.Length > 0)
                {
                    UniqueCodeAttribute attr = objAttrs[0] as UniqueCodeAttribute;
                    if (attr != null)
                    {
                        //自定义特性唯一性校验
                        T existT = GetFirstEntityByFieldValue(propInfo.Name, propInfo.GetValue(entity));
                        if (existT != null)
                        {
                            //编号重复，请输入新的正确编号
                            throw new Exception("Common.ExistedCode");
                        }
                    }
                }
            }
            return DbContext.Insert(entity);
        }

        #endregion

        #region 批量插入

        /// <summary>
        /// 批量插入，不带有事务控制
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert(List<T> entities)
        {
            if (entities == null || entities.Count == 0) return 0;
            try
            {
                entities.ForEach(t =>
                {
                    if (string.IsNullOrEmpty(t.GetPrimaryKeyValue()?.ToString()))
                    {
                        t.SetDefaultValueToPrimaryKey();
                    }
                });

                DbContext.InsertRange(entities);
                return entities.Count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        #endregion

        #region 批量插入带事务
        /// <summary>
        /// 批量插入带有事务处理
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int InsertWithTran(List<T> entities)
        {
            int iCount = 0;
            try
            {
                DbContext.Session.BeginTransaction();
                foreach (T entity in entities)
                {
                    if (string.IsNullOrEmpty(entity.GetPrimaryKeyValue()?.ToString()))
                    {
                        entity.SetDefaultValueToPrimaryKey();
                    }
                    DbContext.Insert<T>(entity);
                    iCount++;
                }
                DbContext.Session.CommitTransaction();
            }
            catch (Exception ex)
            {

                DbContext.Session.RollbackTransaction();
                iCount = 0;
                throw new Exception(ex.Message);
            }

            return iCount;
        }

        #endregion

        #region 大数据批量插入

        /// <summary>
        /// 批量快速添加，不带有事务控制
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BulkInsert(List<T> entities)
        {
            base.BulkInsert(entities);
        }

        #endregion

        #endregion

        #region 更新(完毕)

        /// <summary>
        /// 单个实体更新
        /// </summary>
        /// <param name="entity"></param>
        public int Update(T entity)
        {
            return this.DbContext.Update(entity);
        }

        /// <summary>
        /// 多个实体更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>返回更新行数</returns>
        public int Update(List<T> entities)
        {
            var iCount = 0;
            try
            {
                foreach (T entity in entities)
                {
                    this.DbContext.Update(entity);
                    iCount++;
                }
            }
            catch (Exception ex)
            {
                iCount = 0;
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        internal object GetPageList<SysUser>(Pagination page, Expression<Func<SysUser, bool>> condition)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 多个实体更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int UpdateWithTran(List<T> entities)
        {
            var iCount = 0;
            try
            {
                this.DbContext.Session.BeginTransaction();
                foreach (T entity in entities)
                {
                    this.DbContext.Update(entity);
                    iCount++;
                }
                this.DbContext.Session.CommitTransaction();
            }
            catch (Exception ex)
            {
                iCount = 0;
                this.DbContext.Session.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 根据条件单个实体更新
        /// <para>如：Update(entity,u =>u.id==1);</para>
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Update(T entity, Expression<Func<T, bool>> condition)
        {
            return this.DbContext.Update(entity, condition);

        }

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public int Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            return DbContext.Update<T>(where, entity);
        }




        #endregion

        #region 逻辑删除(完毕)

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Delete(T obj)
        {
            DbContext.TrackEntity(obj);
            obj.IsDelete = true;
            obj.DTime = DateTime.Now;
            return DbContext.Update<T>(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Delete(List<T> entities)
        {
            int iCount = 0;
            try
            {
                entities.ForEach(delegate (T t)
                {
                    DbContext.TrackEntity(t);
                    t.IsDelete = true;
                    t.DTime = DateTime.Now;
                    DbContext.Update(t);
                    iCount++;
                });
            }
            catch (Exception ex)
            {
                iCount = 0;
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int DeleteWithTran(List<T> entities)
        {
            int iCount = 0;
            try
            {
                this.DbContext.Session.BeginTransaction();
                entities.ForEach(delegate (T t)
                {
                    DbContext.TrackEntity(t);
                    t.IsDelete = true;
                    t.DTime = DateTime.Now;
                    DbContext.Update(t);
                    iCount++;
                });
                this.DbContext.Session.CommitTransaction();
            }
            catch (Exception ex)
            {
                iCount = 0;
                this.DbContext.Session.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">待实体列表</param>
        /// <param name="deleteUser">删除者唯一账号</param>
        /// <returns></returns>
        public int Delete(List<T> entities, string deleteUser)
        {
            int iCount = 0;
            try
            {
                entities.ForEach(delegate (T t)
                {
                    DbContext.TrackEntity(t);
                    t.IsDelete = true;
                    t.DTime = DateTime.Now;
                    t.DUser = deleteUser;
                    DbContext.Update(t);
                    iCount++;
                });

            }
            catch (Exception ex)
            {
                iCount = 0;
                throw new Exception(ex.Message);
            }
            return iCount;
        }




        /// <summary>
        /// 删除多个实体 带事务
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="deleteUser"></param>
        /// <returns></returns>
        public int DeleteWithTran(List<T> entities, string deleteUser)
        {
            int iCount = 0;
            try
            {
                this.DbContext.Session.BeginTransaction();
                entities.ForEach(delegate (T t)
                {
                    DbContext.TrackEntity(t);
                    t.IsDelete = true;
                    t.DTime = DateTime.Now;
                    t.DUser = deleteUser;
                    DbContext.Update(t);
                    iCount++;
                });
                this.DbContext.Session.CommitTransaction();
            }
            catch (Exception ex)
            {
                iCount = 0;
                this.DbContext.Session.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            return iCount;
        }


        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="deleteUser"></param>
        /// <returns></returns>
        public int DeleteByKey(object key, string deleteUser)
        {
            try
            {
                T obj = DbContext.QueryByKey<T>(key, true);
                DbContext.TrackEntity(obj);

                obj.IsDelete = true;
                obj.DUser = deleteUser;
                obj.DTime = DateTime.Now;

                return DbContext.Update(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 根据主键数组逻辑删除（必须是主键，且单一主键）
        /// </summary>
        /// <param name="keys">主键数组</param>
        /// <param name="deleteUser">删除者账号</param>
        /// <returns></returns>
        public int DeleteByKeys(object[] keys, string deleteUser)
        {

            int iCount = 0;
            try
            {
                foreach (object key in keys)
                {
                    DeleteByKey(key, deleteUser);
                    iCount++;
                }
            }
            catch (Exception ex)
            {
                iCount = 0;
                throw new Exception(ex.Message);
            }

            return iCount;
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keys">主键数组</param>
        /// <param name="deleteUser">删除者账号</param>
        /// <returns></returns>
        public int DeleteByKeysWithTran(object[] keys, string deleteUser)
        {

            int iCount = 0;
            try
            {
                this.DbContext.BeginTransaction();
                foreach (object key in keys)
                {
                    DeleteByKey(key, deleteUser);
                    iCount++;
                }
                this.DbContext.Session.RollbackTransaction();
            }
            catch (Exception ex)
            {
                iCount = 0;
                this.DbContext.Session.RollbackTransaction();
                throw new Exception(ex.Message);
            }

            return iCount;
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <param name="deleteUser">删除者账号</param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> where, string deleteUser)
        {
            return DbContext.Update(where, a => new T() { IsDelete = true, DTime = DateTime.Now, DUser = deleteUser });
        }



        #endregion

        #region 物理删除(完毕)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int HardDelete(T entity)
        {
            return this.DbContext.Delete(entity);
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int HardDelete(List<T> entities)
        {
            int iCount = 0;
            try
            {
                entities.ForEach(delegate (T entity)
                {
                    this.DbContext.Delete(entity);
                    iCount++;
                });
            }
            catch (Exception ex)
            {
                iCount = 0;
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int HardDeleteWithTran(List<T> entities)
        {
            int iCount = 0;
            try
            {
                this.DbContext.BeginTransaction();
                entities.ForEach(delegate (T entity)
                {
                    this.DbContext.Delete(entity);
                    iCount++;
                });
                this.DbContext.Session.CommitTransaction();
            }
            catch (Exception ex)
            {
                iCount = 0;
                this.DbContext.Session.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 根据主键物理删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int HardDeleteByKey(object key)
        {
            return this.DbContext.DeleteByKey<T>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int HardDeleteByKeys(object[] keys)
        {
            int iCount = 0;
            try
            {
                foreach (object key in keys)
                {
                    HardDeleteByKey(key);
                    iCount++;
                }
            }
            catch (Exception ex)
            {
                iCount = 0;
                throw new Exception(ex.Message);
            }
            return iCount;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public int HardDeleteByKeysWithTran(object[] keys)
        {
            int iCount = 0;
            try
            {
                this.DbContext.BeginTransaction();
                foreach (object key in keys)
                {
                    HardDeleteByKey(key);
                    iCount++;
                }

                this.DbContext.Session.CommitTransaction();
            }
            catch (Exception ex)
            {
                iCount = 0;
                this.DbContext.Session.RollbackTransaction();
                throw new Exception(ex.Message);
            }
            return iCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int HardDelete(Expression<Func<T, bool>> where)
        {
            return this.DbContext.Delete(where);
        }

        #endregion

        #region 获取单个实体(完毕)

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetEntityByKey(object key)
        {
            T entity = DbContext.QueryByKey<T>(key);
            return entity == null || entity.IsDelete ? null : entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetFirstEntity(Expression<Func<T, bool>> where)
        {
            return this.DbContext.Query<T>(where).Where(t => !t.IsDelete).FirstOrDefault();
        }

        #endregion

        #region 是否存在

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> condition)
        {
            var query = DbContext.Query<T>()
                    .Where(x => !x.IsDelete)   //排除已经逻辑删除的记录
                    .Any(condition);
            return query;
        }


        #endregion

        #region 获取列表

        /// <summary>
        /// 按条件查询，默认按主键列排序
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public IQuery<T> GetList(Expression<Func<T, bool>> condition)
        {
            var query = DbContext.Query<T>()
                    .Where(x => !x.IsDelete)   //排除已经逻辑删除的记录
                    .Where(condition)
                    .OrderBy("CTime desc");

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQuery<T> GetList()
        {
            return DbContext.Query<T>()
                    .Where(x => !x.IsDelete);   //排除已经逻辑删除的记录
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<T> GetListByKeys(object[] keys)
        {
            List<T> list = new List<T>();
            foreach (object key in keys)
            {
                T item = GetEntityByKey(key);
                if (item != null)
                {
                    list.Add(item);
                }

            }
            //return DbContext.Query<T>(t=>keys.Contains(t.))

            return list;
        }


        #endregion

        #region 获取分页列表

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <param name="lockType"></param>
        /// <returns></returns>
        public IQuery<T> GetPageList(Pagination page, Expression<Func<T, bool>> condition, LockType lockType)
        {
            if (string.IsNullOrEmpty(page.Sort))
            {
                page.Sort = "CTime desc";
            }
            else
            {
                page.Sort = page.Sort.Replace("ascending", "asc").Replace("descending", "desc");
            }

            var query = DbContext.Query<T>(lockType)
                .Where(x => !x.IsDelete)
                .Where(condition)
                .OrderBy(page.Sort);

            int total = query.Count();
            page.Total = total;
            if (page.PageSize > total)
            {
                return query.TakePage(page.PageNumber, total);
            }
            return query.TakePage(page.PageNumber, page.PageSize);
        }

        /// <summary>
        /// 获取分页数据，默认按照创建时间降序
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQuery<T> GetPageList(Pagination page, Expression<Func<T, bool>> condition)
        {
            if (string.IsNullOrEmpty(page.Sort))
            {
                page.Sort = "CTime desc";
            }
            else
            {
                page.Sort = page.Sort.Replace("ascending", "asc").Replace("descending", "desc");
            }

            var query = DbContext.Query<T>()
                .Where(x => !x.IsDelete)
                .Where(condition)
                .OrderBy(page.Sort);

            int total = query.Count();
            page.Total = total;
            if (page.PageSize > total)
            {
                return query.TakePage(page.PageNumber, total);
            }
            return query.TakePage(page.PageNumber, page.PageSize);
        }

        /// <summary>
        /// 获取分页数据，默认按照创建时间降序
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IQuery<T> GetPageList(Pagination page)
        {
            if (string.IsNullOrEmpty(page.Sort))
            {
                page.Sort = "CTime desc";
            }
            else
            {
                page.Sort = page.Sort.Replace("ascending", "asc").Replace("descending", "desc");
            }
            var query = DbContext.Query<T>()
                .Where(x => !x.IsDelete)
                .OrderBy(page.Sort);

            int total = query.Count();
            page.Total = total;
            if (page.PageSize > total)
            {
                return query.TakePage(page.PageNumber, total);
            }
            return query.TakePage(page.PageNumber, page.PageSize);
        }




        #endregion

        #region 自主分页
        /// <summary>
        /// 由于分页无法满足一些特殊需求，可以将已经查询出来的数据进行分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsData"></param>
        /// <returns></returns>
        public List<T> GetPageListBySelf<T>(Pagination page, List<T> itemsData)
        {
            int total = itemsData.Count();
            page.Total = total;
            var itemPageData = new List<T>();
            if (page.PageSize > total)
            {
                itemPageData = itemsData.Skip(page.PageSize * (page.PageNumber - 1)).Take(total)?.ToList();
            }
            else
            {
                itemPageData = itemsData.Skip(page.PageSize * (page.PageNumber - 1)).Take(page.PageSize)?.ToList();
            }
            return itemPageData;
        }
        #endregion

        #region 获取列表包含已删除数据

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQuery<T> GetAllList()
        {
            return DbContext.Query<T>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQuery<T> GetAllList(Expression<Func<T, bool>> condition)
        {
            var query = DbContext.Query<T>()
                    .Where(condition);
            return query;
        }



        #endregion

        #region 获取分页列表（包含删除数据）

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IQuery<T> GetAllPageList(Pagination page, Expression<Func<T, bool>> condition)
        {
            if (string.IsNullOrEmpty(page.Sort))
            {
                page.Sort = "CTime desc";
            }
            else
            {
                page.Sort = page.Sort.Replace("ascending", "asc").Replace("descending", "desc");
            }
            var query = DbContext.Query<T>()
                .Where(condition)
                .OrderBy(page.Sort);

            int total = query.Count();
            page.Total = total;
            if (page.PageSize > total)
            {
                return query.TakePage(page.PageNumber, total);
            }
            return query.TakePage(page.PageNumber, page.PageSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IQuery<T> GetAllPageList(Pagination page)
        {
            if (string.IsNullOrEmpty(page.Sort))
            {
                page.Sort = "CTime desc";
            }
            else
            {
                page.Sort = page.Sort.Replace("ascending", "asc").Replace("descending", "desc");
            }
            var query = DbContext.Query<T>()
                .OrderBy(page.Sort);

            int total = query.Count();
            page.Total = total;
            if (page.PageSize > total)
            {
                return query.TakePage(page.PageNumber, total);
            }
            return query.TakePage(page.PageNumber, page.PageSize);
        }

        #endregion

        #region 存储过程

        /// <summary>
        /// 执行存储过程返回实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object SqlQuery(string procName, System.Data.CommandType cmdType, params DbParam[] parameters)
        {
            return this.DbContext.SqlQuery<object>(procName, cmdType, parameters).ToList();
        }





        #endregion

        #region 通过指定字段和值，作为查询条件，获取实体

        /// <summary>
        /// 通过指定字段和值，作为查询条件，获取实体
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public T GetFirstEntityByFieldValue(string field, object value)
        {
            return this.DbContext.SqlQuery<T>("select * from " + typeof(T).Name + " where IsDelete=0 and " + field + "=@value", DbParam.Create("@value", value)).FirstOrDefault();
        }

        #endregion

        #region 通过指定字段和值，作为查询条件，获取实体

        /// <summary>
        /// 通过指定字段和值，作为查询条件，获取实体
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public T GetFirstEntityByFieldsValues(string[] fields, object[] values)
        {
            DbParamList pList = new DbParamList();
            StringBuilder sb = new StringBuilder("select * from ");
            sb.Append(typeof(T).Name).Append(" where IsDelete=0 ");
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i];
                sb.Append(" and ").Append(field).Append("=@").Append(field);
                pList.Add(field, values[i]);
            }

            return this.DbContext.SqlQuery<T>(sb.ToString(), pList).FirstOrDefault();
        }

        #endregion

        #region 返回DataTable扩展
        /// <summary>
        /// 返回DataTable扩展
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="objParams"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string cmdText, object objParams)
        {
            return DbContext.Session.ExecuteDataTable(cmdText, objParams);
        }


        #endregion


        #region 获取实体类的字段
        public object GetProperties()
        {
            T t = new T();
            return GetProperties(t);
        }
        public object GetDescriptions()
        {
            T t = new T();
            List<string> list = new List<string>();
            foreach (var item in GetProperties(t))
            {
                string colDescription = AttributeUtil.GetPropertyDescriptionAttributeValue<T>(item);
                list.Add(string.IsNullOrEmpty(colDescription) ? item : colDescription);   // 字段中文描述,未设置则取字段名
            }
            return list;
        }
        /// <summary>
        /// 获取类中的属性
        /// </summary>
        /// <returns>所有属性名称</returns>
        private List<string> GetProperties<T>(T t)
        {
            List<string> ListStr = new List<string>();
            if (t == null)
            {
                return ListStr;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return ListStr;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name; //名称
                object value = item.GetValue(t, null);  //值

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    ListStr.Add(name);
                }
                else
                {
                    GetProperties(value);
                }
            }
            return ListStr;
        }
        #endregion
    }
}