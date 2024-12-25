using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class OrgDeptConfiguration : IEntityTypeConfiguration<OrgDept>
    {
        public void Configure(EntityTypeBuilder<OrgDept> builder)
        {
            builder.ToTable("org_dept");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Code).HasColumnName("code").HasComment("部门编号");
            builder.Property(x => x.Name).HasColumnName("name").HasComment("部门名称");
            builder.Property(x => x.Sort).HasColumnName("sort").HasComment("排序");
            builder.Property(x => x.Description).HasColumnName("description").HasComment("描述");
            builder.Property(x => x.Status).HasColumnName("status").HasComment("状态：1正常2停用");
            builder.Property(x => x.CuratorId).HasColumnName("curatorId").HasComment("负责人");
            builder.Property(x => x.Email).HasColumnName("email").HasComment("邮箱");
            builder.Property(x => x.Phone).HasColumnName("phone").HasComment("电话");
            builder.Property(x => x.ParentId).HasColumnName("parent_id").HasComment("父ID");
            builder.Property(x => x.ParentIds).HasColumnName("parent_ids").HasComment("层级IDS");
            builder.Property(x => x.Layer).HasColumnName("layer").HasComment("层级");
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