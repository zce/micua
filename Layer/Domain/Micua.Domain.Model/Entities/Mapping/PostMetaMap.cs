// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : PostMeta（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// PostMeta（实体映射类）
    /// </summary>
    internal partial class PostMetaMap : EntityTypeConfiguration<PostMeta>
    {
        public PostMetaMap()
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
            this.ToTable(MicuaContext.GetTableName("PostMeta"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.PostId).HasColumnName(MicuaContext.GetColumnName("PostId"));
            this.Property(t => t.Key).HasColumnName(MicuaContext.GetColumnName("Key"));
            this.Property(t => t.Value).HasColumnName(MicuaContext.GetColumnName("Value"));

            // Relationships
            this.HasRequired(t => t.Post)
                .WithMany(t => t.PostMetas)
                .HasForeignKey(d => d.PostId)
                .WillCascadeOnDelete(false);
        }
    }
}
