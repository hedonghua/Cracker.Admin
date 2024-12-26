using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cracker.Admin.Entities;

namespace Cracker.Admin.EntitiesConfiguration
{
    public class OrgPositionConfiguration : IEntityTypeConfiguration<OrgPosition>
    {
        public void Configure(EntityTypeBuilder<OrgPosition> builder)
        {
            builder.ToTable("org_position");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Code).HasColumnName("code").HasComment("职位编号");
            builder.Property(x => x.Name).HasColumnName("name").HasComment("职位名称");
            builder.Property(x => x.Level).HasColumnName("level").HasComment("职级");
            builder.Property(x => x.Description).HasColumnName("description").HasComment("描述");
            builder.Property(x => x.Status).HasColumnName("status").HasComment("状态：1正常2停用");
            builder.Property(x => x.GroupId).HasColumnName("group_id").HasComment("职位分组ID");
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