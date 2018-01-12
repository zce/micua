// +----------------------------------------------------------------------
// | Micua [ DO IT BATTER ! ]
// +----------------------------------------------------------------------
// | Copyright © 2014 Wedn.Net. All Rights Reserved.
// +----------------------------------------------------------------------
// | Summary : User（实体映射类）
// +----------------------------------------------------------------------
// | Author  : iceStone <ice@wedn.net>
// +----------------------------------------------------------------------
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Micua.Domain.Model.Mapping
{
    /// <summary>
    /// User（实体映射类）
    /// </summary>
    internal partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Login)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(64);

            this.Property(t => t.Nickname)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Mobile)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Link)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(255);

            this.Property(t => t.Introduce)
                .IsRequired();

            this.Property(t => t.Token)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable(MicuaContext.GetTableName("User"));
            this.Property(t => t.Id).HasColumnName(MicuaContext.GetColumnName("Id"));
            this.Property(t => t.Login).HasColumnName(MicuaContext.GetColumnName("Login"));
            this.Property(t => t.Password).HasColumnName(MicuaContext.GetColumnName("Password"));
            this.Property(t => t.Nickname).HasColumnName(MicuaContext.GetColumnName("Nickname"));
            this.Property(t => t.Mobile).HasColumnName(MicuaContext.GetColumnName("Mobile"));
            this.Property(t => t.Email).HasColumnName(MicuaContext.GetColumnName("Email"));
            this.Property(t => t.Link).HasColumnName(MicuaContext.GetColumnName("Link"));
            this.Property(t => t.Introduce).HasColumnName(MicuaContext.GetColumnName("Introduce"));
            this.Property(t => t.Role).HasColumnName(MicuaContext.GetColumnName("Role"));
            this.Property(t => t.Status).HasColumnName(MicuaContext.GetColumnName("Status"));
            this.Property(t => t.Registered).HasColumnName(MicuaContext.GetColumnName("Registered"));
            this.Property(t => t.Token).HasColumnName(MicuaContext.GetColumnName("Token"));
        }
    }
}
