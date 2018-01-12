// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : TermRelation（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// TermRelation（实体映射类）
    /// </summary>
    internal partial class TermRelationMap : EntityTypeConfiguration<TermRelation>
    {
        public TermRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("TermRelation"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.ObjectId).HasColumnName(MicuaContext.GetColumnName("ObjectId"));
            this.Property(t => t.Type).HasColumnName(MicuaContext.GetColumnName("Type"));
            this.Property(t => t.TermId).HasColumnName(MicuaContext.GetColumnName("TermId"));
            this.Property(t => t.Sort).HasColumnName(MicuaContext.GetColumnName("Sort"));

            // Relationships
            this.HasRequired(t => t.Term)
                .WithMany(t => t.Relations)
                .HasForeignKey(d => d.TermId)
                .WillCascadeOnDelete(false);
        }
    }
}
