using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Micua.Infrastructure.Utility
{
    /// <summary>
    /// SQL Server数据库访问助手类
    /// 本类为静态类 不可以被实例化 需要使用时直接调用即可
    /// Copyright © 2013 Wedn.Net
    /// </summary>
    public static partial class SqlHelper
    {
        private static readonly string[] localhost = new[] { "localhost", ".", "(local)" };
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private readonly static string connStr;
        static SqlHelper()
        {
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings["MicuaContext"];
            if (conn != null)
            {
                connStr = conn.ConnectionString;
            }
        }

        #region 数据库检测

        #region 测试数据库服务器连接 +static bool TestConnection(string host, int port, int millisecondsTimeout)
        /// <summary> 
        /// 采用Socket方式，测试数据库服务器连接 
        /// </summary> 
        /// <param name="host">服务器主机名或IP</param> 
        /// <param name="port">端口号</param> 
        /// <param name="millisecondsTimeout">等待时间：毫秒</param> 
        /// <exception cref="Exception">链接异常</exception>
        /// <returns></returns> 
        public static bool TestConnection(string host, int port, int millisecondsTimeout)
        {
            host = localhost.Contains(host) ? "127.0.0.1" : host;
            using (var client = new TcpClient())
            {
                try
                {
                    var ar = client.BeginConnect(host, port, null, null);
                    ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
                    return client.Connected;
                    //await client.ConnectAsync(host, port);
                    //return client.Connected;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        #endregion

        #region 检测表是否存在 + static bool ExistsTable(string table)
        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="table">要检测的表名</param>
        /// <returns></returns>
        public static bool ExistsTable(string table)
        {
            string sql = "select count(1) from sysobjects where id = object_id(N'[" + table + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object res = ExecuteScalar(sql);
            return (Object.Equals(res, null)) || (Object.Equals(res, System.DBNull.Value));
        }
        #endregion

        #region 判断是否存在某张表的某个字段 +static bool ExistsColumn(string table, string column)
        /// <summary>
        /// 判断是否存在某张表的某个字段
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="column">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ExistsColumn(string table, string column)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('N[" + table + "]') and [name]='" + column + "'";
            object res = ExecuteScalar(sql);
            if (res == null)
                return false;
            return Convert.ToInt32(res) > 0;
        }
        #endregion

        #region 判断某张表的某个字段中是否存在某个值 +static bool ColumnExistsValue(string table, string column, string value)
        /// <summary>
        /// 判断某张表的某个字段中是否存在某个值
        /// </summary>
        /// <param name="table">表名称</param>
        /// <param name="column">列名称</param>
        /// <param name="value">要判断的值</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExistsValue(string table, string column, string value)
        {
            string sql = "SELECT count(1) FROM [" + table + "] WHERE [" + column + "]=@Value;";
            object res = ExecuteScalar(sql, new SqlParameter("@Value", value));
            if (res == null)
                return false;
            return Convert.ToInt32(res) > 0;
        }
        #endregion

        #endregion

        #region 公共方法

        #region 获取指定表中指定字段的最大值, 确保字段为INT类型
        /// <summary>
        /// 获取指定表中指定字段的最大值, 确保字段为INT类型
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="tableName">表名</param>
        /// <returns>最大值</returns>
        public static int QueryMaxId(string fieldName, string tableName)
        {
            string sql = string.Format("select max([{0}]) from [{1}];", fieldName, tableName);
            object res = ExecuteScalar(sql);
            if (res == null)
                return 0;
            return Convert.ToInt32(res);
        }
        #endregion

        #region 生成查询语句

        #region 生成分页查询数据库语句 +static string GenerateQuerySql(string table, string[] columns, int index, int size, string wheres, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成分页查询数据库语句, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合, 多个列用英文逗号分割(colum1,colum2...)</param>
        /// <param name="index">页码(即第几页)(传入-1则表示忽略分页取出全部)</param>
        /// <param name="size">显示页面大小(即显示条数)</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySql(string table, string[] columns, int index, int size, string where, string orderField, bool isDesc = true)
        {
            if (index == 1)
            {
                // 生成查询第一页SQL
                return GenerateQuerySql(table, columns, size, where, orderField, isDesc);
            }
            if (index < 1)
            {
                // 取全部数据
                return GenerateQuerySql(table, columns, where, orderField, isDesc);
            }
            if (string.IsNullOrEmpty(orderField))
            {
                throw new ArgumentNullException("orderField");
            }
            // 其他情况, 生成row_number分页查询语句
            // SQL模版
            const string format = @"select {0} from
                                    (
                                        select ROW_NUMBER() over(order by [{1}] {2}) as num, {0} from [{3}] {4}
                                    )
                                    as tbl
                                    where
                                        tbl.num between ({5}-1)*{6} + 1 and {5}*{6};";
            // where语句组建
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            // 查询字段拼接
            string column = columns != null && columns.Any() ? string.Join(" , ", columns) : "*";
            return string.Format(format, column, orderField, isDesc ? "desc" : string.Empty, table, where, index, size);
        }
        #endregion
        #region 生成查询数据库语句查询全部 +static string GenerateQuerySql(string table, string[] columns, string wheres, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成查询数据库语句查询全部, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySql(string table, string[] columns, string where, string orderField, bool isDesc = true)
        {
            // where语句组建
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            // 查询字段拼接
            string column = columns != null && columns.Any() ? string.Join(" , ", columns) : "*";
            const string format = "select {0} from [{1}] {2} {3} {4}";
            return string.Format(format, column, table, where,
                string.IsNullOrEmpty(orderField) ? string.Empty : "order by " + orderField,
                isDesc && !string.IsNullOrEmpty(orderField) ? "desc" : string.Empty);
        }
        #endregion

        #region 生成分页查询数据库语句查询第一页 +static string GenerateQuerySql(string table, string[] columns, int size, string where, string orderField, bool isDesc = true)
        /// <summary>
        /// 生成分页查询数据库语句查询第一页, 返回生成的T-SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="columns">列集合</param>
        /// <param name="size">显示页面大小(即显示条数)</param>
        /// <param name="where">条件语句(忽略则传入null)</param>
        /// <param name="orderField">排序字段(即根据那个字段排序)(忽略则传入null)</param>
        /// <param name="isDesc">排序方式(0:降序(desc)|1:升序(asc))(忽略则传入-1)</param>
        /// <returns>生成的T-SQL语句</returns>
        public static string GenerateQuerySql(string table, string[] columns, int size, string where, string orderField, bool isDesc = true)
        {
            // where语句组建
            where = string.IsNullOrEmpty(where) ? string.Empty : "where " + where;
            // 查询字段拼接
            string column = columns != null && columns.Any() ? string.Join(" , ", columns) : "*";
            const string format = "select top {0} {1} from [{2}] {3} {4} {5}";
            return string.Format(format, size, column, table, where,
                  string.IsNullOrEmpty(orderField) ? string.Empty : "order by " + orderField,
                  isDesc && !string.IsNullOrEmpty(orderField) ? "desc" : string.Empty);
        }
        #endregion
        #endregion

        #region 将一个SqlDataReader对象转换成一个实体类对象 +static TEntity MapEntity<TEntity>(SqlDataReader reader) where TEntity : class,new()
        /// <summary>
        /// 将一个SqlDataReader对象转换成一个实体类对象
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="reader">当前指向的reader</param>
        /// <returns>实体对象</returns>
        public static TEntity MapEntity<TEntity>(SqlDataReader reader) where TEntity : class,new()
        {
            try
            {
                var props = typeof(TEntity).GetProperties();
                var entity = new TEntity();
                foreach (var prop in props)
                {
                    if (prop.CanWrite)
                    {
                        try
                        {
                            var index = reader.GetOrdinal(prop.Name);
                            var data = reader.GetValue(index);
                            if (data != DBNull.Value)
                            {
                                prop.SetValue(entity, Convert.ChangeType(data, prop.PropertyType), null);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            continue;
                        }
                    }
                }
                return entity;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #endregion

        #region SQL执行方法

        #region ExecuteNonQuery +static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteNonQuery +static int ExecuteNonQuery(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个非查询的T-SQL语句，返回受影响行数，如果执行的是非增、删、改操作，返回-1
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    try
                    {
                        conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return res;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region ExecuteScalar +static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteScalar(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteScalar +static object ExecuteScalar(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句，返回第一行第一列的结果
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>返回第一行第一列的数据</returns>
        public static object ExecuteScalar(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    try
                    {
                        conn.Open();
                        object res = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        return res;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region ExecuteReader +static void ExecuteReader(string cmdText, Action<SqlDataReader> action, params SqlParameter[] parameters)
        /// <summary>
        /// 利用委托 执行一个大数据查询的T-SQL语句
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="action">传入执行的委托对象</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        public static void ExecuteReader(string cmdText, Action<SqlDataReader> action, params SqlParameter[] parameters)
        {
            ExecuteReader(cmdText, action, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteReader +static void ExecuteReader(string cmdText, Action<SqlDataReader> action, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 利用委托 执行一个大数据查询的T-SQL语句
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="action">传入执行的委托对象</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        public static void ExecuteReader(string cmdText, Action<SqlDataReader> action, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.CommandType = type;
                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            action(reader);
                        }
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        conn.Close();
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region ExecuteReader +static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个SqlDataReader对象, 如果出现SQL语句执行错误, 将会关闭连接通道抛出异常
        ///  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteReader(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteReader +static SqlDataReader ExecuteReader(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个SqlDataReader对象, 如果出现SQL语句执行错误, 将会关闭连接通道抛出异常
        ///  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <exception cref="链接数据库异常"></exception>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connStr);
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                if (parameters != null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = type;
                conn.Open();
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return reader;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    //出现异常关闭连接并且释放
                    conn.Close();
                    throw ex;
                }
            }
        }
        #endregion

        #region ExecuteDataSet +static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个离线数据集DataSet
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>离线数据集DataSet</returns>
        public static DataSet ExecuteDataSet(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteDataSet(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteDataSet +static DataSet ExecuteDataSet(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个查询的T-SQL语句, 返回一个离线数据集DataSet
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>离线数据集DataSet</returns>
        public static DataSet ExecuteDataSet(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmdText, connStr))
            {
                using (DataSet ds = new DataSet())
                {
                    if (parameters != null)
                    {
                        adapter.SelectCommand.Parameters.Clear();
                        adapter.SelectCommand.Parameters.AddRange(parameters);
                    }
                    adapter.SelectCommand.CommandType = type;
                    adapter.Fill(ds);
                    adapter.SelectCommand.Parameters.Clear();
                    return ds;
                }
            }
        }
        #endregion

        #region ExecuteDataTable +static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个数据表查询的T-SQL语句, 返回一个DataTable
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>查询到的数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
        {
            return ExecuteDataTable(cmdText, CommandType.Text, parameters);
        }
        #endregion

        #region ExecuteDataTable +static DataTable ExecuteDataTable(string cmdText, CommandType type, params SqlParameter[] parameters)
        /// <summary>
        /// 执行一个数据表查询的T-SQL语句, 返回一个DataTable
        /// </summary>
        /// <param name="cmdText">要执行的T-SQL语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>查询到的数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, CommandType type, params SqlParameter[] parameters)
        {
            return ExecuteDataSet(cmdText, type, parameters).Tables[0];
        }
        #endregion
        #endregion
    }
}
