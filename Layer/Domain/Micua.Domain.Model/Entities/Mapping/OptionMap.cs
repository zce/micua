// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : Option（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// Option（实体映射类）
    /// </summary>
    internal partial class OptionMap : EntityTypeConfiguration<Option>
    {
        public OptionMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("Option"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Name).HasColumnName(MicuaContext.GetColumnName("Name"));
            this.Property(t => t.Value).HasColumnName(MicuaContext.GetColumnName("Value"));
            this.Property(t => t.Enabled).HasColumnName(MicuaContext.GetColumnName("Enabled"));
        }
    }
}
