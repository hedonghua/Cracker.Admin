using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class SysRoleMenuConfiguration : IEntityTypeConfiguration<SysRoleMenu>
    {
        public void Configure(EntityTypeBuilder<SysRoleMenu> builder)
        {
            builder.ToTable("sys_role_menu");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.MenuId).HasColumnName("menu_id").HasComment("菜单ID");
            builder.Property(x => x.RoleId).HasColumnName("role_id").HasComment("角色ID");
        }
    }
}