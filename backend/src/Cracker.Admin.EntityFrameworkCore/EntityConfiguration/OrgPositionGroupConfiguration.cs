using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cracker.Admin.Entities;

namespace Cracker.Admin.EntitiesConfiguration
{
    public class OrgPositionGroupConfiguration : IEntityTypeConfiguration<OrgPositionGroup>
    {
        public void Configure(EntityTypeBuilder<OrgPositionGroup> builder)
        {
            builder.ToTable("org_position_group");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.GroupName).HasColumnName("group_name").HasComment("职位名");
            builder.Property(x => x.Remark).HasColumnName("remark").HasComment("备注");
            builder.Property(x => x.ParentId).HasColumnName("parent_id").HasComment("父级ID");
            builder.Property(x => x.ParentIds).HasColumnName("parent_ids").HasComment("层级父ID");
            //abp字段
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.CreatorId).HasColumnName("creator_id");
            builder.Property(x => x.LastModificationTime).HasColumnName("last_modification_time");
            builder.Property(x => x.LastModifierId).HasColumnName("last_modifier_id");
        }
    }
}