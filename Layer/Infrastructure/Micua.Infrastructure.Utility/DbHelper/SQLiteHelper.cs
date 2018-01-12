//using System;
//using System.Configuration;
//using System.Data;
//using System.Data.SQLite;


//namespace Micua.Infrastructure.Utility
//{
//    /// <summary>
//    /// SQLite帮助类
//    /// </summary>
//    public class SQLiteHelper : System.IDisposable
//    {
//        private SQLiteConnection _SQLiteConn = null;
//        private SQLiteTransaction _SQLiteTrans = null;
//        private bool _IsRunTrans = false;
//        private string _SQLiteConnString = null;
//        private bool _disposed = false;
//        private bool _autocommit = false;
//        #region 构造/析构函数
//        /// <summary>
//        /// 初始化 SQLiteHelper

//        /// </summary>
//        public SQLiteHelper()
//            : this(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString)
//        {
//        }
//        /// <summary>
//        /// 初始化 SQLiteHelper
//        /// </summary>
//        /// <param name="connectionstring">数据库连接字符串</param>
//        public SQLiteHelper(string connectionstring)
//        {
//            this._SQLiteConnString = connectionstring;
//            this._SQLiteConn = new SQLiteConnection(this._SQLiteConnString);
//            this._SQLiteConn.Commit += new SQLiteCommitHandler(_SQLiteConn_Commit);
//            this._SQLiteConn.RollBack += new EventHandler(_SQLiteConn_RollBack);
//        }

//        /// <summary>
//        /// SQLiteHelper 析构函数
//        /// </summary>
//        ~SQLiteHelper()
//        {
//            this.Dispose(false);
//        }

//        #endregion
//        #region 方法
//        /// <summary>
//        /// 打开数据库连接
//        /// </summary>
//        private void Open()
//        {
//            if (this._SQLiteConn.State == ConnectionState.Closed)
//            {
//                this._SQLiteConn.Open();
//            }
//        }
//        /// <summary>
//        /// 关闭数据库连接
//        /// </summary>
//        private void Close()
//        {
//            if (this._SQLiteConn.State != ConnectionState.Closed)
//            {
//                if (this._IsRunTrans && this._autocommit)
//                {
//                    this.Commit();
//                }
//                this._SQLiteConn.Close();
//            }
//        }
//        /// <summary>
//        /// 开始数据库事务
//        /// </summary>
//        public void BeginTransaction()
//        {
//            this._SQLiteConn.BeginTransaction();
//            this._IsRunTrans = true;
//        }
//        /// <summary>
//        /// 开始数据库事务
//        /// </summary>
//        /// <param name="isoLevel">事务锁级别</param>
//        public void BeginTransaction(IsolationLevel isoLevel)
//        {
//            this._SQLiteConn.BeginTransaction(isoLevel);
//            this._IsRunTrans = true;
//        }
//        /// <summary>
//        /// 提交当前挂起的事务
//        /// </summary>
//        public void Commit()
//        {
//            if (this._IsRunTrans)
//            {
//                this._SQLiteTrans.Commit();
//                this._IsRunTrans = false;
//            }
//        }
//        /// <summary>
//        /// 回滚当前挂起的事务
//        /// </summary>
//        public void Rollback()
//        {
//            if (this._IsRunTrans)
//            {
//                this._SQLiteTrans.Rollback();
//                this._IsRunTrans = false;
//            }
//        }
//        /// <summary>
//        /// 执行SQL语句
//        /// </summary>
//        /// <param name="command">SQL语句</param>
//        /// <returns>返回受影响行数 [SELECT 不会返回影响行]</returns>
//        public int Execute(string command)
//        {
//            int result = -1;
//            this.Open();
//            using (SQLiteCommand sqlitecmd = new SQLiteCommand(command))
//            {
//                result = sqlitecmd.ExecuteNonQuery();
//            }
//            this.Close();
//            return result;
//        }
//        /// <summary>
//        /// 执行SQL语句
//        /// </summary>
//        /// <param name="command">SQL语句</param>
//        /// <param name="parameter">参数组</param>
//        /// <returns>返回受影响行数 [SELECT 不会返回影响行]</returns>
//        public int Execute(string command, SQLiteParameter[] parameter)
//        {
//            int result = -1;
//            this.Open();
//            using (SQLiteCommand sqlitecmd = new SQLiteCommand(command))
//            {
//                sqlitecmd.Parameters.AddRange(parameter);
//                result = sqlitecmd.ExecuteNonQuery();
//            }
//            this.Close();
//            return result;
//        }
//        /// <summary>
//        /// 执行SQL语句
//        /// </summary>
//        /// <param name="command">SQL语句</param>
//        /// <returns>返回第一行第一列值</returns>
//        public object ExecuteScalar(string command)
//        {
//            object result = null;
//            this.Open();
//            using (SQLiteCommand sqlitecmd = new SQLiteCommand(command))
//            {
//                result = sqlitecmd.ExecuteScalar();
//            }
//            this.Close();
//            return result;
//        }
//        /// <summary>
//        /// 执行SQL语句
//        /// </summary>
//        /// <param name="command">SQL语句</param>
//        /// <param name="parmeter">参数组</param>
//        /// <returns>返回受影响行数 [SELECT 不会返回影响行]</returns>
//        public object ExecuteScalar(string command, SQLiteParameter[] parmeter)
//        {
//            object result = null;
//            this.Open();
//            using (SQLiteCommand sqlitecmd = new SQLiteCommand(command))
//            {
//                sqlitecmd.Parameters.AddRange(parmeter);
//                result = sqlitecmd.ExecuteScalar();
//            }
//            this.Close();
//            return result;
//        }
//        /// <summary>
//        /// 执行SQL语句
//        /// </summary>
//        /// <param name="command">SQL语句</param>
//        /// <returns>返回DataSet数据集</returns>
//        public DataSet GetDs(string command)
//        {
//            return this.GetDs(command, string.Empty);
//        }
//        public DataSet GetDs(string command, string tablename)
//        {
//            DataSet ds = new DataSet();
//            this.Open();
//            using (SQLiteCommand sqlitecmd = new SQLiteCommand(command, this._SQLiteConn))
//            {
//                using (SQLiteDataAdapter sqliteadapter = new SQLiteDataAdapter(sqlitecmd))
//                {
//                    if (string.Empty.Equals(tablename))
//                    {
//                        sqliteadapter.Fill(ds);
//                    }
//                    else
//                    {
//                        sqliteadapter.Fill(ds, tablename);
//                    }
//                }
//            }
//            this.Close();
//            return ds;
//        }

//        public DataSet GetDs(string command, out SQLiteCommand SqlItecmd)
//        {
//            return this.GetDs(command, string.Empty, out SqlItecmd);
//        }

//        public DataSet GetDs(string command, string tablename, out SQLiteCommand SqlItecmd)
//        {
//            DataSet ds = new DataSet();
//            this.Open();
//            SQLiteCommand sqlcmd = new SQLiteCommand(command, this._SQLiteConn);
//            using (SQLiteDataAdapter sqladapter = new SQLiteDataAdapter(sqlcmd))
//            {
//                sqladapter.Fill(ds);
//            }
//            SqlItecmd = sqlcmd;
//            this.Close();
//            return ds;
//        }

//        public int Update(DataSet ds, ref SQLiteCommand SqlItecmd)
//        {
//            return this.Update(ds, string.Empty, ref SqlItecmd);
//        }

//        public int Update(DataSet ds, string tablename, ref SQLiteCommand SqlItecmd)
//        {
//            int result = -1;
//            this.Open();
//            using (SQLiteDataAdapter sqladapter = new SQLiteDataAdapter(SqlItecmd))
//            {
//                using (SQLiteCommandBuilder sqlcommandbuilder = new SQLiteCommandBuilder(sqladapter))
//                {
//                    if (string.Empty.Equals(tablename))
//                    {
//                        result = sqladapter.Update(ds);
//                    }
//                    else
//                    {
//                        result = sqladapter.Update(ds, tablename);
//                    }
//                }
//            }
//            this.Close();
//            return result;
//        }
//        /// <summary>
//        /// 释放该实例的托管资源
//        /// </summary>
//        public virtual void Dispose()
//        {
//            this.Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        protected void Dispose(bool disposing)
//        {
//            if (!this._disposed)
//            {
//                if (disposing)
//                {
//                    // 定义释放非托管资源
//                }
//                this._disposed = true;
//            }
//        }
//        #endregion
//        #region 属性
//        /// <summary>
//        /// 获取数据库连接字符串
//        /// </summary>
//        public string ConnectionString
//        {
//            get
//            {
//                return this._SQLiteConnString;
//            }
//        }
//        /// <summary>
//        /// 设置是否自动提交事务
//        /// </summary>
//        public bool AutoCommit
//        {
//            get
//            {
//                return this._autocommit;
//            }
//            set
//            {
//                this._autocommit = value;
//            }
//        }
//        #endregion
//        #region 事件
//        void _SQLiteConn_RollBack(object sender, EventArgs e)
//        {
//            this._IsRunTrans = false;
//        }
//        void _SQLiteConn_Commit(object sender, CommitEventArgs e)
//        {
//            this._IsRunTrans = false;
//        }
//        #endregion
//    }
//}