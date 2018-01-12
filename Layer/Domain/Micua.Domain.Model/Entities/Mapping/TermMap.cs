// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Term（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// Term（实体映射类）
    /// </summary>
    internal partial class TermMap : EntityTypeConfiguration<Term>
    {
        public TermMap()
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
                .IsUnicode(false)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("Term"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Slug).HasColumnName(MicuaContext.GetColumnName("Slug"));
            this.Property(t => t.Name).HasColumnName(MicuaContext.GetColumnName("Name"));
            this.Property(t => t.Description).HasColumnName(MicuaContext.GetColumnName("Description"));
            this.Property(t => t.Type).HasColumnName(MicuaContext.GetColumnName("Type"));
            this.Property(t => t.Sort).HasColumnName(MicuaContext.GetColumnName("Sort"));
            this.Property(t => t.Count).HasColumnName(MicuaContext.GetColumnName("Count"));
            this.Property(t => t.ParentId).HasColumnName(MicuaContext.GetColumnName("ParentId")).IsOptional();

            // Relationships
            this.HasOptional(t => t.Parent)
                .WithMany(t => t.Children)
                .HasForeignKey(d => d.ParentId)
                .WillCascadeOnDelete(false);
        }
    }
}
