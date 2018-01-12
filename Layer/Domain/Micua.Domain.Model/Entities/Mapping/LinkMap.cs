// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Link（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// Link（实体映射类）
    /// </summary>
    internal partial class LinkMap : EntityTypeConfiguration<Link>
    {
        public LinkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Image)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Target)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            this.Property(t => t.Relation)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("Link"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Name).HasColumnName(MicuaContext.GetColumnName("Name"));
            this.Property(t => t.Url).HasColumnName(MicuaContext.GetColumnName("Url"));
            this.Property(t => t.Image).HasColumnName(MicuaContext.GetColumnName("Image"));
            this.Property(t => t.Title).HasColumnName(MicuaContext.GetColumnName("Title"));
            this.Property(t => t.Target).HasColumnName(MicuaContext.GetColumnName("Target"));
            this.Property(t => t.Relation).HasColumnName(MicuaContext.GetColumnName("Relation"));
            this.Property(t => t.Type).HasColumnName(MicuaContext.GetColumnName("Type"));
            this.Property(t => t.Visible).HasColumnName(MicuaContext.GetColumnName("Visible"));
            this.Property(t => t.Modified).HasColumnName(MicuaContext.GetColumnName("Modified"));
            this.Property(t => t.Sort).HasColumnName(MicuaContext.GetColumnName("Sort"));
            this.Property(t => t.BlogId).HasColumnName(MicuaContext.GetColumnName("BlogId"));
            this.Property(t => t.ParentId).HasColumnName(MicuaContext.GetColumnName("ParentId"));

            // Relationships
            this.HasRequired(t => t.Blog)
                .WithMany(t => t.Links)
                .HasForeignKey(d => d.BlogId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
