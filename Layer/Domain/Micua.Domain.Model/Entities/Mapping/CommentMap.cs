// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Comment（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// Comment（实体映射类）
    /// </summary>
    internal partial class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Link)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("Comment"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Author).HasColumnName(MicuaContext.GetColumnName("Author"));
            this.Property(t => t.Email).HasColumnName(MicuaContext.GetColumnName("Email"));
            this.Property(t => t.Link).HasColumnName(MicuaContext.GetColumnName("Link"));
            this.Property(t => t.Commented).HasColumnName(MicuaContext.GetColumnName("Commented"));
            this.Property(t => t.Content).HasColumnName(MicuaContext.GetColumnName("Content"));
            this.Property(t => t.Status).HasColumnName(MicuaContext.GetColumnName("Status"));
            this.Property(t => t.UserId).HasColumnName(MicuaContext.GetColumnName("UserId"));
            this.Property(t => t.BlogId).HasColumnName(MicuaContext.GetColumnName("BlogId"));
            this.Property(t => t.PostId).HasColumnName(MicuaContext.GetColumnName("PostId"));
            this.Property(t => t.ParentId).HasColumnName(MicuaContext.GetColumnName("ParentId")).IsOptional();

            // Relationships
            this.HasRequired(t => t.Blog)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.BlogId)
                .WillCascadeOnDelete(false);
            this.HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.Post)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.PostId)
                .WillCascadeOnDelete(false);
            this.HasRequired(t => t.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
