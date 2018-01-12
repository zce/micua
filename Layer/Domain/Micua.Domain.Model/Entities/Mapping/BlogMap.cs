// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Blog（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// Blog（实体映射类）
    /// </summary>
    internal partial class BlogMap : EntityTypeConfiguration<Blog>
    {
        public BlogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Slug)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SubName)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("Blog"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Slug).HasColumnName(MicuaContext.GetColumnName("Slug"));
            this.Property(t => t.Name).HasColumnName(MicuaContext.GetColumnName("Name"));
            this.Property(t => t.SubName).HasColumnName(MicuaContext.GetColumnName("SubName"));
            this.Property(t => t.Created).HasColumnName(MicuaContext.GetColumnName("Created"));
            this.Property(t => t.Status).HasColumnName(MicuaContext.GetColumnName("Status"));
            this.Property(t => t.UserId).HasColumnName(MicuaContext.GetColumnName("UserId"));

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Blogs)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
