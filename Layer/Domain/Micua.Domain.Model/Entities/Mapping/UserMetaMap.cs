// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : UserMeta（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// UserMeta（实体映射类）
    /// </summary>
    internal partial class UserMetaMap : EntityTypeConfiguration<UserMeta>
    {
        public UserMetaMap()
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
            this.ToTable(MicuaContext.GetTableName("UserMeta"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.UserId).HasColumnName(MicuaContext.GetColumnName("UserId"));
            this.Property(t => t.Key).HasColumnName(MicuaContext.GetColumnName("Key"));
            this.Property(t => t.Value).HasColumnName(MicuaContext.GetColumnName("Value"));

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserMetas)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
