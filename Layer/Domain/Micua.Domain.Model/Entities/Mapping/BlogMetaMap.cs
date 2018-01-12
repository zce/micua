// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : BlogMeta（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// BlogMeta（实体映射类）
    /// </summary>
    internal partial class BlogMetaMap : EntityTypeConfiguration<BlogMeta>
    {
        public BlogMetaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Key)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("BlogMeta"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.BlogId).HasColumnName(MicuaContext.GetColumnName("BlogId"));
            this.Property(t => t.Key).HasColumnName(MicuaContext.GetColumnName("Key"));
            this.Property(t => t.Value).HasColumnName(MicuaContext.GetColumnName("Value"));

            // Relationships
            this.HasRequired(t => t.Blog)
                .WithMany(t => t.BlogMetas)
                .HasForeignKey(d => d.BlogId)
                .WillCascadeOnDelete(false);
        }
    }
}
