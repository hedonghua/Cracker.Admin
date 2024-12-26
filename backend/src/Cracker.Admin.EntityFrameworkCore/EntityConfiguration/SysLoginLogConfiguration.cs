using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cracker.Admin.Entities;

namespace Cracker.Admin.EntitiesConfiguration
{
    public class SysLoginLogConfiguration : IEntityTypeConfiguration<SysLoginLog>
    {
        public void Configure(EntityTypeBuilder<SysLoginLog> builder)
        {
            builder.ToTable("sys_login_log");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserName).HasColumnName("user_name").HasComment("账号");
            builder.Property(x => x.Ip).HasColumnName("ip").HasComment("IP");
            builder.Property(x => x.Address).HasColumnName("address").HasComment("登录地址");
            builder.Property(x => x.Os).HasColumnName("os").HasComment("系统");
            builder.Property(x => x.Browser).HasColumnName("browser").HasComment("浏览器");
            builder.Property(x => x.OperationMsg).HasColumnName("operation_msg").HasComment("操作信息");
            builder.Property(x => x.IsSuccess).HasColumnName("is_success").HasComment("是否成功");
            //abp字段
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.CreatorId).HasColumnName("creator_id");
            builder.Property(x => x.LastModificationTime).HasColumnName("last_modification_time");
            builder.Property(x => x.LastModifierId).HasColumnName("last_modifier_id");
        }
    }
}