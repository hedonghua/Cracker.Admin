using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class OrgEmployeeConfiguration : IEntityTypeConfiguration<OrgEmployee>
    {
        public void Configure(EntityTypeBuilder<OrgEmployee> builder)
        {
            builder.ToTable("org_employee");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Code).HasColumnName("code").HasComment("工号");
            builder.Property(x => x.Name).HasColumnName("name").HasComment("姓名");
            builder.Property(x => x.Sex).HasColumnName("Sex").HasComment("性别");
            builder.Property(x => x.Phone).HasColumnName("phone").HasComment("手机号码");
            builder.Property(x => x.IdNo).HasColumnName("idno").HasComment("身份证");
            builder.Property(x => x.FrontIdNoUrl).HasColumnName("front_idno_url").HasComment("身份证正面");
            builder.Property(x => x.BackIdNoUrl).HasColumnName("back_idno_url").HasComment("身份证背面");
            builder.Property(x => x.Address).HasColumnName("address").HasComment("现住址");
            builder.Property(x => x.Email).HasColumnName("email").HasComment("邮箱");
            builder.Property(x => x.BirthDay).HasColumnName("birthday").HasComment("生日");
            builder.Property(x => x.InTime).HasColumnName("in_time").HasComment("入职时间");
            builder.Property(x => x.OutTime).HasColumnName("out_time").HasComment("离职时间");
            builder.Property(x => x.IsOut).HasColumnName("is_out").HasComment("是否离职");
            builder.Property(x => x.UserId).HasColumnName("user_id").HasComment("关联用户ID");
            builder.Property(x => x.DeptId).HasColumnName("dept_id").HasComment("部门ID");
            builder.Property(x => x.PositionId).HasColumnName("position_id").HasComment("职位ID");
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