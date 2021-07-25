using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Entity;
using System.Linq.Expressions;
using Chloe.SqlServer;

namespace MJ.Application
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ContentBase : IDisposable
    {
        IDbContext _dbContext;

        private string _dbKey;

        /// <summary>
        /// 
        /// </summary>
        protected ContentBase()
        {
            
            this._dbKey = "DbConnection";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbKey"></param>
        protected ContentBase(string dbKey)
        {
            this._dbKey = dbKey;
            
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IDbContext DbContext
        {
            get
            {
                if (this._dbContext == null)
                    this._dbContext = new MsSqlContext(DbConfig.GetDbConnectionString(this._dbKey));
                this._dbContext.Session.CommandTimeout = 300;
                //IDbCommandInterceptor interceptor = new DbCommandInterceptor();
                //_dbContext.Session.AddInterceptor(interceptor);
                return this._dbContext;
            }
            set
            {
                this._dbContext = value;
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public void BulkInsert<T>(List<T> entities)
        {
            
            DbContext.BulkInsert(entities);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }




        #region 连接WMS中间库

        IDbContext _dbContextForSRM;

        private string _dbKeyForSRM = "DbConnectionForWMS";

        /// <summary>
        /// 构造函数-WMS数据库
        /// </summary>
        public IDbContext DbContextForWMS
        {
            get
            {
                if (this._dbContextForSRM == null)
                    this._dbContextForSRM = new MsSqlContext(DbConfig.GetDbConnectionStringForWMS(this._dbKeyForSRM));
                this._dbContextForSRM.Session.CommandTimeout = 300;
                //IDbCommandInterceptor interceptor = new DbCommandInterceptor();
                //_dbContext.Session.AddInterceptor(interceptor);
                return this._dbContextForSRM;
            }
            set
            {
                this._dbContextForSRM = value;
            }
        }

        #endregion

        #region 连接SAP中间库

        IDbContext _dbContextForSAP;

        private string _dbKeyForSAP = "DbConnectionForSAP";

        /// <summary>
        /// 构造函数-WMS数据库
        /// </summary>
        public IDbContext DbContextForSAP
        {
            get
            {
                if (this._dbContextForSRM == null)
                    this._dbContextForSRM = new MsSqlContext(DbConfig.GetDbConnectionStringForWMS(this._dbKeyForSAP));
                this._dbContextForSRM.Session.CommandTimeout = 300;
                //IDbCommandInterceptor interceptor = new DbCommandInterceptor();
                //_dbContext.Session.AddInterceptor(interceptor);
                return this._dbContextForSRM;
            }
            set
            {
                this._dbContextForSRM = value;
            }
        }

        #endregion


    }
}
