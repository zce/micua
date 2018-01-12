// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Post（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// Post（实体映射类）
    /// </summary>
    internal partial class PostMap : EntityTypeConfiguration<Post>
    {
        public PostMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Slug)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.Content)
                .IsRequired();

            this.Property(t => t.Excerpt)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("Post"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Slug).HasColumnName(MicuaContext.GetColumnName("Slug"));
            this.Property(t => t.Author).HasColumnName(MicuaContext.GetColumnName("Author"));
            this.Property(t => t.Title).HasColumnName(MicuaContext.GetColumnName("Title"));
            this.Property(t => t.Published).HasColumnName(MicuaContext.GetColumnName("Published"));
            this.Property(t => t.Modified).HasColumnName(MicuaContext.GetColumnName("Modified"));
            this.Property(t => t.Content).HasColumnName(MicuaContext.GetColumnName("Content"));
            this.Property(t => t.Excerpt).HasColumnName(MicuaContext.GetColumnName("Excerpt"));
            this.Property(t => t.Type).HasColumnName(MicuaContext.GetColumnName("Type"));
            this.Property(t => t.Status).HasColumnName(MicuaContext.GetColumnName("Status"));
            this.Property(t => t.CommentStatus).HasColumnName(MicuaContext.GetColumnName("CommentStatus"));
            this.Property(t => t.PingStatus).HasColumnName(MicuaContext.GetColumnName("PingStatus"));
            this.Property(t => t.Sort).HasColumnName(MicuaContext.GetColumnName("Sort"));
            this.Property(t => t.ViewCount).HasColumnName(MicuaContext.GetColumnName("ViewCount"));
            this.Property(t => t.CommentCount).HasColumnName(MicuaContext.GetColumnName("CommentCount"));
            this.Property(t => t.UserId).HasColumnName(MicuaContext.GetColumnName("UserId"));
            this.Property(t => t.BlogId).HasColumnName(MicuaContext.GetColumnName("BlogId"));
            this.Property(t => t.ParentId).HasColumnName(MicuaContext.GetColumnName("ParentId")).IsOptional();

            // Relationships
            this.HasRequired(t => t.Blog)
                .WithMany(t => t.Posts)
                .HasForeignKey(d => d.BlogId)
                .WillCascadeOnDelete(false);
            this.HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Posts)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
