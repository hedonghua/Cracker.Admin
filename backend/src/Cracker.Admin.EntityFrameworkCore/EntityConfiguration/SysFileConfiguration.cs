using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Cracker.Admin.Entity;

namespace Cracker.Admin.EntityConfiguration
{
    public class SysFileConfiguration : IEntityTypeConfiguration<SysFile>
    {
        public void Configure(EntityTypeBuilder<SysFile> builder)
        {
            builder.ToTable("sys_file");
            builder.HasKey(x => x.Id).HasName("id");
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.FileName).HasColumnName("file_name").HasComment("文件名");
            builder.Property(x => x.UniqueName).HasColumnName("unique_name").HasComment("文件编号/BLOB名");
            builder.Property(x => x.Size).HasColumnName("size").HasComment("单位KB");
            builder.Property(x => x.Suffix).HasColumnName("suffix").HasComment("后缀");
            builder.Property(x => x.RelativeUrl).HasColumnName("relative_url").HasComment("后缀");
            builder.Property(x => x.MimeType).HasColumnName("mime_type").HasComment("媒体类型");
            builder.Property(x => x.ExtraPath).HasColumnName("extra_path").HasComment("额外地址");
            builder.Property(x => x.UploadType).HasColumnName("upload_type").HasComment("上传类型 9:BLOB文件系统");
            //abp字段
            builder.Property(x => x.CreationTime).HasColumnName("creation_time");
            builder.Property(x => x.CreatorId).HasColumnName("creator_id");
        }
    }
}