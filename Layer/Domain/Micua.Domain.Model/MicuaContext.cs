// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : MicuaContext（数据库上下文）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------

namespace Micua.Domain.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using Micua.Domain.Model.Mapping;

    /// <summary>
    /// MicuaContext（数据库上下文）
    /// </summary>
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    //[DbConfigurationType(typeof(System.Data.Entity.DbConfiguration))]
    public partial class MicuaContext : DbContext
    {
        #region Fields

        /// <summary>
        /// 唤醒状态
        /// </summary>
        static bool IsWarmUp;

        /// <summary>
        /// 表名前缀
        /// </summary>
        internal static string TablePrefix;

        /// <summary>
        /// 表名是否转换成复数
        /// </summary>
        internal static bool TableNameToPlural;

        /// <summary>
        /// 列名前缀
        /// </summary>
        internal static string ColumnPrefix;

        #endregion

        #region Constructors

        /// <summary>
        /// 静态构造函数，用于初始化数据库、暖机等初始化操作
        /// </summary>
        static MicuaContext()
        {
            // 如果数据库不存在则创建数据库
            // Database.SetInitializer(new CreateDatabaseIfNotExists<MicuaContext>());
            // 模型发生变化时先删除数据库再创建数据库
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MicuaContext>());
            // 总是先删除数据库再创建数据库
            // Database.SetInitializer(new DropCreateDatabaseAlways<MicuaContext>());
            // 无操作
            Database.SetInitializer<MicuaContext>(null);
        }

        /// <summary>
        /// 可以将给定字符串用作将连接到的数据库的名称或连接字符串来构造一个新的上下文实例。请参见有关这如何用于创建连接的类备注。
        /// </summary>
        public MicuaContext() : this("name=MicuaContext") { }

        /// <summary>
        /// 可以将给定字符串用作将连接到的数据库的名称或连接字符串来构造一个新的上下文实例。请参见有关这如何用于创建连接的类备注。
        /// </summary>
        /// <param name="nameOrConnectionString">连接字符串名称或连接字符串</param>
        /// <param name="tablePrefix">表名前缀（默认为空）</param>
        /// <param name="tableToPlural">表名是否转换成复数（默认为false）</param>
        /// <param name="columnPrefix">列名前缀（默认为空）</param>
        public MicuaContext(string nameOrConnectionString, string tablePrefix = "", bool tableToPlural = false, string columnPrefix = "")
            : base(nameOrConnectionString)
        {
            WarmUp(nameOrConnectionString);
            TablePrefix = tablePrefix;
            TableNameToPlural = tableToPlural;
            ColumnPrefix = columnPrefix;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set Blog DbSet.
        /// </summary>
        public DbSet<Blog> Blogs { get; set; }
        /// <summary>
        /// Get or set BlogMeta DbSet.
        /// </summary>
        public DbSet<BlogMeta> BlogMetas { get; set; }
        /// <summary>
        /// Get or set Comment DbSet.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// Get or set CommentMeta DbSet.
        /// </summary>
        public DbSet<CommentMeta> CommentMetas { get; set; }
        /// <summary>
        /// Get or set Link DbSet.
        /// </summary>
        public DbSet<Link> Links { get; set; }
        /// <summary>
        /// Get or set Option DbSet.
        /// </summary>
        public DbSet<Option> Options { get; set; }
        /// <summary>
        /// Get or set Post DbSet.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        /// <summary>
        /// Get or set PostMeta DbSet.
        /// </summary>
        public DbSet<PostMeta> PostMetas { get; set; }
        /// <summary>
        /// Get or set Term DbSet.
        /// </summary>
        public DbSet<Term> Terms { get; set; }
        /// <summary>
        /// Get or set TermRelation DbSet.
        /// </summary>
        public DbSet<TermRelation> TermRelations { get; set; }
        /// <summary>
        /// Get or set User DbSet.
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Get or set UserMeta DbSet.
        /// </summary>
        public DbSet<UserMeta> UserMetas { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，将调用此方法。
        /// 虽然此方法的默认实现不执行任何操作，但可在派生类中重写此方法，这样便能在锁定模型之前对其进行进一步的配置。
        /// </summary>
        /// <param name="modelBuilder">定义要创建的上下文的模型的生成器。</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlogMap());
            modelBuilder.Configurations.Add(new BlogMetaMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new CommentMetaMap());
            modelBuilder.Configurations.Add(new LinkMap());
            modelBuilder.Configurations.Add(new OptionMap());
            modelBuilder.Configurations.Add(new PostMap());
            modelBuilder.Configurations.Add(new PostMetaMap());
            modelBuilder.Configurations.Add(new TermMap());
            modelBuilder.Configurations.Add(new TermRelationMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserMetaMap());
        }

        /// <summary>
        /// EntityFramework预热操作
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public static void WarmUp(string nameOrConnectionString)
        {
            if (!IsWarmUp) return;
            //EF暖机操作;
            using (MicuaContext db = new MicuaContext(nameOrConnectionString))
            {
                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                var mappingCollection = (System.Data.Entity.Core.Mapping.StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(System.Data.Entity.Core.Metadata.Edm.DataSpace.CSSpace);
                mappingCollection.GenerateViews(new System.Collections.Generic.List<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError>());
            }
            IsWarmUp = true;
        }

        /// <summary>
        /// 根据表名规则生成表名
        /// </summary>
        /// <param name="input">实体名</param>
        /// <returns>表名</returns>
        public static string GetTableName(string input)
        {
            //return input;
            var ps = System.Data.Entity.Design.PluralizationServices.PluralizationService.CreateService(System.Globalization.CultureInfo.GetCultureInfo("en-us"));
            if (TableNameToPlural && ps.IsSingular(input))
            {
                input = ps.Pluralize(input);
            }
            return TablePrefix.ToLower() + input.ToUnderlineCase();
        }

        /// <summary>
        /// 根据列名规则生成列名
        /// </summary>
        /// <param name="input">属性名</param>
        /// <returns>列名</returns>
        public static string GetColumnName(string input)
        {
            //return input;
            return ColumnPrefix.ToLower() + input.ToUnderlineCase();
        }

        #endregion
    }
}

