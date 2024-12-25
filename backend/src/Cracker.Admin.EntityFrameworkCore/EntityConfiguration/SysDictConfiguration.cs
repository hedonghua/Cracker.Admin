using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class SysDictConfiguration : IEntityTypeConfiguration<SysDict>
    {
        public void Configure(EntityTypeBuilder<SysDict> builder)
        {
            builder.ToTable("sys_dict");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Key).HasColumnName("key").HasComment("字典键");
            builder.Property(x => x.Value).HasColumnName("value").HasComment("字典值");
            builder.Property(x => x.Label).HasColumnName("label").HasComment("显示文本");
            builder.Property(x => x.GroupName).HasColumnName("group_name").HasComment("组名");
            builder.Property(x => x.Remark).HasColumnName("remark").HasComment("备注");
            builder.Property(x => x.Sort).HasColumnName("sort").HasComment("排序值");
            builder.Property(x => x.IsEnabled).HasColumnName("is_enabled").HasComment("是否开启");
            //abp字段
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