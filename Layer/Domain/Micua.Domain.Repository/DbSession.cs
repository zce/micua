// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbSession.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   数据库操作会话级对象
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Micua.Domain.Repository
{
    using System;
    using System.Data.Entity;
    using System.Runtime.Remoting.Messaging;
    using Micua.Domain.Model;
    using Micua.Infrastructure.Utility;

    /// <summary>
    /// 数据库操作会话级对象
    /// </summary>
    public class DbSession : Singleton<DbSession>, IDisposable
    {
        //static ConnectionString ConnectionString { get; set; }
        //static DbSession()
        //{
        //    ConnectionString = new ConnectionString
        //    {
        //        DataSource = Config.GetString("db_server"),
        //        InitialCatalog = Config.GetString("db_name"),
        //        IntegratedSecurity = Config.GetBoolean("db_integrated_auth"),
        //        UserID = Config.GetString("db_user"),
        //        Password = Config.GetString("db_password"),
        //        MinPoolSize = Config.GetInt32("db_min_pool_size"),
        //        MaxPoolSize = Config.GetInt32("db_max_pool_size"),
        //        ConnectTimeout = Config.GetInt32("db_connect_timeout")
        //    };
        //    var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath); //System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        //    var connStrs = config.ConnectionStrings;
        //    connStrs.ConnectionStrings.Remove("MicuaContext");
        //    connStrs.ConnectionStrings.Add(new System.Configuration.ConnectionStringSettings("MicuaContext", ConnectionString.ToString(), Config.GetString("db_provider_name")));
        //    config.Save(System.Configuration.ConfigurationSaveMode.Modified);
        //}

        /// <summary>
        /// Prevents a default instance of the <see cref="DbSession"/> class from being created.
        /// </summary>
        private DbSession() { }

        //private DbContext _dbContext;
        /// <summary>
        /// Gets the db context.
        /// </summary>
        public DbContext DbContext
        {
            get
            {
                // NB的单例模式，跨时代的产品！（线程内实例唯一）
                var db = CallContext.GetData("micua_db_context") as DbContext;
                if (db != null)
                {
                    return db;
                }

                db = new MicuaContext("name=MicuaContext", Config.GetString("db_table_prefix"), Config.GetBoolean("db_table_plural", false), Config.GetString("db_column_prefix"));
                CallContext.SetData("micua_db_context", db);
                return db;
            }
        }

        #region Repositories
        private IBlogRepository _blog;
        /// <summary>
        /// BlogRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IBlogRepository BlogRepository
        {
            get
            {
                return _blog ?? (_blog = new BlogRepository());
            }
        }
        private IBlogMetaRepository _blogmeta;
        /// <summary>
        /// BlogMetaRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IBlogMetaRepository BlogMetaRepository
        {
            get
            {
                return _blogmeta ?? (_blogmeta = new BlogMetaRepository());
            }
        }
        private ICommentRepository _comment;
        /// <summary>
        /// CommentRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public ICommentRepository CommentRepository
        {
            get
            {
                return _comment ?? (_comment = new CommentRepository());
            }
        }
        private ICommentMetaRepository _commentmeta;
        /// <summary>
        /// CommentMetaRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public ICommentMetaRepository CommentMetaRepository
        {
            get
            {
                return _commentmeta ?? (_commentmeta = new CommentMetaRepository());
            }
        }
        private ILinkRepository _link;
        /// <summary>
        /// LinkRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public ILinkRepository LinkRepository
        {
            get
            {
                return _link ?? (_link = new LinkRepository());
            }
        }
        private IOptionRepository _option;
        /// <summary>
        /// OptionRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IOptionRepository OptionRepository
        {
            get
            {
                return _option ?? (_option = new OptionRepository());
            }
        }
        private IPostRepository _post;
        /// <summary>
        /// PostRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IPostRepository PostRepository
        {
            get
            {
                return _post ?? (_post = new PostRepository());
            }
        }
        private IPostMetaRepository _postmeta;
        /// <summary>
        /// PostMetaRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IPostMetaRepository PostMetaRepository
        {
            get
            {
                return _postmeta ?? (_postmeta = new PostMetaRepository());
            }
        }
        private ITermRepository _term;
        /// <summary>
        /// TermRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public ITermRepository TermRepository
        {
            get
            {
                return _term ?? (_term = new TermRepository());
            }
        }
        private ITermRelationRepository _termrelation;
        /// <summary>
        /// TermRelationRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public ITermRelationRepository TermRelationRepository
        {
            get
            {
                return _termrelation ?? (_termrelation = new TermRelationRepository());
            }
        }
        private IUserRepository _user;
        /// <summary>
        /// UserRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IUserRepository UserRepository
        {
            get
            {
                return _user ?? (_user = new UserRepository());
            }
        }
        private IUserMetaRepository _usermeta;
        /// <summary>
        /// UserMetaRepository 实例
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2014-01-05 17:20 Modified By iceStone
        /// </remarks>
        public IUserMetaRepository UserMetaRepository
        {
            get
            {
                return _usermeta ?? (_usermeta = new UserMetaRepository());
            }
        }
        #endregion

        /// <summary>
        /// 保存数据库的改变状态
        /// </summary>
        /// <returns>受影响行数</returns>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2013-12-21 20:16 Modified By iceStone
        /// </remarks>
        public int SaveChanges()
        {
            return DbContext != null ? DbContext.SaveChanges() : 0;
            //db = new MicuaModelContainer();
            //CallContext.SetData(MicuaStruct.CurrentDbContextKey, db);
            //return db.SaveChanges();
        }

        /// <summary>
        /// 释放数据库上下文
        /// </summary>
        /// <remarks>
        ///  2013-11-18 19:06 Created By iceStone
        ///  2013-12-21 20:16 Modified By iceStone
        /// </remarks>
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
