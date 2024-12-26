using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cracker.Admin.Entities;

namespace Cracker.Admin.EntitiesConfiguration
{
    public class SysBusinessLogConfiguration : IEntityTypeConfiguration<SysBusinessLog>
    {
        public void Configure(EntityTypeBuilder<SysBusinessLog> builder)
        {
            builder.ToTable("sys_business_log");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.UserName).HasColumnName("user_name").HasComment("账号");
            builder.Property(x => x.Action).HasColumnName("action").HasComment("操作方法，全名");
            builder.Property(x => x.HttpMethod).HasColumnName("http_method").HasComment("http请求方式");
            builder.Property(x => x.Url).HasColumnName("url").HasComment("请求地址");
            builder.Property(x => x.Os).HasColumnName("os").HasComment("系统");
            builder.Property(x => x.Browser).HasColumnName("browser").HasComment("浏览器");
            builder.Property(x => x.NodeName).HasColumnName("node_name").HasComment("操作节点名");
            builder.Property(x => x.Ip).HasColumnName("ip").HasComment("IP");
            builder.Property(x => x.Address).HasColumnName("address").HasComment("登录地址");
            builder.Property(x => x.IsSuccess).HasColumnName("is_success").HasComment("是否成功");
            builder.Property(x => x.OperationMsg).HasColumnName("operation_msg").HasComment("操作信息");
            builder.Property(x => x.MillSeconds).HasColumnName("mill_seconds").HasComment("耗时，单位毫秒");
            //abp字段
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.CreatorId).HasColumnName("creator_id");
            builder.Property(x => x.LastModificationTime).HasColumnName("last_modification_time");
            builder.Property(x => x.LastModifierId).HasColumnName("last_modifier_id");
        }
    }
}