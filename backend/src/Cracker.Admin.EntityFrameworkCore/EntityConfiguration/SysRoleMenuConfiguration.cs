using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class SysUserRoleConfiguration : IEntityTypeConfiguration<SysUserRole>
    {
        public void Configure(EntityTypeBuilder<SysUserRole> builder)
        {
            builder.ToTable("sys_user_role");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserId).HasColumnName("user_id").HasComment("用户ID");
            builder.Property(x => x.RoleId).HasColumnName("role_id").HasComment("角色ID");
        }
    }
}