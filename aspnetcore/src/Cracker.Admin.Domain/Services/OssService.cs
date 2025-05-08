using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.MultiTenancy;

namespace Cracker.Admin.Services
{
    /// <summary>
    /// 模拟对象存储（存放在宿主服务器）
    /// </summary>
    public class OssService : DomainService
    {
        private readonly IConfiguration configuration;
        private readonly ICurrentTenant currentTenant;

        public OssService(IConfiguration configuration, ICurrentTenant currentTenant)
        {
            this.configuration = configuration;
            this.currentTenant = currentTenant;
        }

        public async Task<string> UploadAsync(byte[] bytes, string fileName, bool rename = true, bool cover = false)
        {
            var rootPath = GetOssRootPath();
            var dir = Path.GetDirectoryName(fileName);
            if (string.IsNullOrWhiteSpace(dir))
            {
                dir = "default";
            }
            var name = Path.GetFileName(fileName);
            var path = Path.Combine(rootPath, dir);
            var extension = Path.GetExtension(name);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var newFileName = name;
            if (rename)
            {
                newFileName = Guid.NewGuid().ToString("N") + extension;
            }
            var fullPath = Path.Combine(path, newFileName);
            if (!File.Exists(fullPath) || cover)
            {
                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    await fs.WriteAsync(bytes);
                    fs.Close();
                }
            }
            return configuration["Oss:Domain"] + dir + "/" + newFileName;
        }

        public async Task<byte[]> GetFileAsync(string fileName)
        {
            var rootPath = GetOssRootPath();
            var path = Path.Combine(rootPath, fileName);
            if (!File.Exists(path)) throw new FileNotFoundException();

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var bytes = new byte[fs.Length];
                fs.Seek(0, SeekOrigin.Begin);
                await fs.ReadAsync(bytes);
                fs.Close();
                return bytes;
            }
        }

        public string GetOssRootPath()
        {
            if (MultiTenancyConsts.IsEnabled && !string.IsNullOrEmpty(currentTenant.Name))
            {
                return Path.Combine(_rooPath(), currentTenant.Name);
            }
            return _rooPath();

            string _rooPath()
            {
                var path = configuration["Oss:Bucket"];
                if (Directory.Exists(path)) return path;
                try
                {
                    Directory.CreateDirectory(path!);
                    return path!;
                }
                catch (Exception)
                {
                    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "oss");
                }
            }
        }
    }
}