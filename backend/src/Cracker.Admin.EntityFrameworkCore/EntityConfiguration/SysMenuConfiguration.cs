using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class SysMenuConfiguration : IEntityTypeConfiguration<SysMenu>
    {
        public void Configure(EntityTypeBuilder<SysMenu> builder)
        {
            builder.ToTable("sys_menu");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Title).HasColumnName("title").HasComment("显示标题/名称");
            builder.Property(x => x.Name).HasColumnName("name").HasComment("组件名");
            builder.Property(x => x.Icon).HasColumnName("icon").HasComment("图标");
            builder.Property(x => x.Path).HasColumnName("path").HasComment("路由/地址");
            builder.Property(x => x.FunctionType).HasColumnName("function_type").HasComment("功能类型");
            builder.Property(x => x.Permission).HasColumnName("permission").HasComment("授权码");
            builder.Property(x => x.ParentId).HasColumnName("parent_id").HasComment("父级ID");
            builder.Property(x => x.Sort).HasColumnName("sort").HasComment("排序");
            builder.Property(x => x.Hidden).HasColumnName("hidden").HasComment("是否隐藏");
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