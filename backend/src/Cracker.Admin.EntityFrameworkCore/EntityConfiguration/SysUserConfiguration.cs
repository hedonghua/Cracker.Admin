using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class SysUserConfiguration : IEntityTypeConfiguration<SysUser>
    {
        public void Configure(EntityTypeBuilder<SysUser> builder)
        {
            builder.ToTable("sys_user");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserName).HasColumnName("username").HasComment("用户名");
            builder.Property(x => x.Password).HasColumnName("password").HasComment("密码");
            builder.Property(x => x.PasswordSalt).HasColumnName("password_salt").HasComment("密码盐");
            builder.Property(x => x.Avatar).HasColumnName("avatar").HasComment("头像");
            builder.Property(x => x.NickName).HasColumnName("nickname").HasComment("昵称");
            builder.Property(x => x.Sex).HasColumnName("sex").HasComment("性别");
            builder.Property(x => x.IsEnabled).HasColumnName("is_enabled").HasComment("是否启用");
            //abp字段
            builder.Property(x => x.ExtraProperties).HasColumnName("extra_properties");
            builder.Property(x => x.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.CreatorId).HasColumnName("creator_id");
            builder.Property(x => x.LastModificationTime).HasColumnName("last_modification_time");
            builder.Property(x => x.LastModifierId).HasColumnName("last_modifier_id");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.DeleterId).HasColumnName("deleter_id");
            builder.Property(x => x.DeletionTime).HasColumnName("deletion_time");
        }
    }
}