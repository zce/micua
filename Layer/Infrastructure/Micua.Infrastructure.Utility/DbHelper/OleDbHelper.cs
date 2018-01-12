using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Micua.Infrastructure.Utility
{
    /// <summary>
    /// 数据库访问助手类
    /// 本类为抽象类 不可以被实例化
    /// 需要使用时直接调用即可
    /// </summary>
    public abstract class OleDbHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        #region ExecuteNonQuery
        /// <summary>
        /// 执行一个非查询的T-OleDb语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType type, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 执行一个非查询的T-OleDb语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params OleDbParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行一个查询的T-OleDb语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, CommandType type, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 执行一个查询的T-OleDb语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, params OleDbParameter[] parameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 利用委托 执行一个大数据查询的T-OleDb语句
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="action">传入执行的委托对象</param>
        /// <param name="parameters">参数列表</param>
        public static void ExecuteReader(string cmdText, CommandType type, Action<OleDbDataReader> action, params OleDbParameter[] parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        action(reader);
                    }
                }
            }
        }

        /// <summary>
        /// 利用委托 执行一个大数据查询的T-OleDb语句
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="action">传入执行的委托对象</param>
        /// <param name="parameters">参数列表</param>
        public static void ExecuteReader(string cmdText, Action<OleDbDataReader> action, params OleDbParameter[] parameters)
        {
            ExecuteReader(cmdText, CommandType.Text, parameters);
        }

        /// <summary>
        /// 执行一个查询的T-OleDb语句, 返回一个OleDbDataReader对象, 如果出现OleDb语句执行错误, 那么抛出异常
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>OleDbDataReader对象</returns>
        public static OleDbDataReader ExecuteReader(string cmdText, CommandType type, params OleDbParameter[] parameters)
        {
            OleDbConnection conn = new OleDbConnection(connStr);
            using (OleDbCommand cmd = new OleDbCommand(cmdText, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = type;
                conn.Open();
                try
                {
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception ex)
                {
                    //出现异常关闭连接并且释放
                    conn.Close();
                    conn.Dispose();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行一个查询的T-OleDb语句, 返回一个OleDbDataReader对象, 如果出现OleDb语句执行错误, 那么抛出异常
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>OleDbDataReader对象</returns>
        public static OleDbDataReader ExecuteReader(string cmdText, params OleDbParameter[] parameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 执行一个查询的T-OleDb语句, 返回一个离线数据集DataSet
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>离线数据集DataSet</returns>
        public static DataSet ExecuteDataSet(string cmdText, CommandType type, params OleDbParameter[] parameters)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmdText, connStr))
            {
                using (DataSet ds = new DataSet())
                {
                    if (parameters != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                    }
                    adapter.SelectCommand.CommandType = type;
                    adapter.Fill(ds);
                    return ds;
                }
            }
        }

        /// <summary>
        /// 执行一个查询的T-OleDb语句, 返回一个离线数据集DataSet
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>离线数据集DataSet</returns>
        public static DataSet ExecuteDataSet(string cmdText, params OleDbParameter[] parameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// 执行一个数据表查询的T-OleDb语句, 返回一个DataTable
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>查询到的数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, CommandType type, params OleDbParameter[] parameters)
        {
            return ExecuteDataSet(cmdText, type, parameters).Tables[0];
        }
        /// <summary>
        /// 执行一个数据表查询的T-OleDb语句, 返回一个DataTable
        /// </summary>
        /// <param name="cmdText">要执行的T-OleDb语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>查询到的数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, params OleDbParameter[] parameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, parameters);
        }
        #endregion
    }
}
