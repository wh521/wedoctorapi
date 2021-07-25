using Chloe.Infrastructure.Interception;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MJ.Core.Log;

namespace MJ.Application
{
    /// <summary>
    /// SQL语句拦截器
    /// </summary>
    public class DbCommandInterceptor : IDbCommandInterceptor
    {
        /* 执行 DbCommand.ExecuteReader() 时调用 */
        public void ReaderExecuting(IDbCommand command, DbCommandInterceptionContext<IDataReader> interceptionContext)
        {
            interceptionContext.DataBag.Add("startTime", DateTime.Now);
            LogUtil.WriteLog(command.CommandText);
        }
        /* 执行 DbCommand.ExecuteReader() 后调用 */
        public void ReaderExecuted(IDbCommand command, DbCommandInterceptionContext<IDataReader> interceptionContext)
        {
            DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            LogUtil.WriteLog(DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString());
            if (interceptionContext.Exception == null)
                LogUtil.WriteLog(interceptionContext.Result.FieldCount.ToString());
        }

        /* 执行 DbCommand.ExecuteNonQuery() 时调用 */
        public void NonQueryExecuting(IDbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            interceptionContext.DataBag.Add("startTime", DateTime.Now);
            LogUtil.WriteLog(command.CommandText);
        }
        /* 执行 DbCommand.ExecuteNonQuery() 后调用 */
        public void NonQueryExecuted(IDbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            LogUtil.WriteLog(DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString());
            if (interceptionContext.Exception == null)
                LogUtil.WriteLog(interceptionContext.Result.ToString());
        }

        /* 执行 DbCommand.ExecuteScalar() 时调用 */
        public void ScalarExecuting(IDbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            interceptionContext.DataBag.Add("startTime", DateTime.Now);
            LogUtil.WriteLog(command.CommandText);
        }
        /* 执行 DbCommand.ExecuteScalar() 后调用 */
        public void ScalarExecuted(IDbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            DateTime startTime = (DateTime)(interceptionContext.DataBag["startTime"]);
            LogUtil.WriteLog(DateTime.Now.Subtract(startTime).TotalMilliseconds.ToString());
            if (interceptionContext.Exception == null)
                LogUtil.WriteLog(interceptionContext.Result.ToString());
        }
    }
}
