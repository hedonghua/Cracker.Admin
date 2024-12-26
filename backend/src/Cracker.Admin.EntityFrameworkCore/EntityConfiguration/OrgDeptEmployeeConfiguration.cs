using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cracker.Admin.Entities;

namespace Cracker.Admin.EntitiesConfiguration
{
    public class OrgDeptEmployeeConfiguration : IEntityTypeConfiguration<OrgDeptEmployee>
    {
        public void Configure(EntityTypeBuilder<OrgDeptEmployee> builder)
        {
            builder.ToTable("org_dept_employee");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.EmployeeId).HasColumnName("employee_id").HasComment("员工ID");
            builder.Property(x => x.DeptId).HasColumnName("dept_id").HasComment("部门ID");
            builder.Property(x => x.PositionId).HasColumnName("position_id").HasComment("职位ID");
            builder.Property(x => x.IsMain).HasColumnName("is_main").HasComment("是否主职位");
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