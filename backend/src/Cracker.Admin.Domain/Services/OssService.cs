using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Volo.Abp.Domain.Services;

namespace Cracker.Admin.Services
{
    public class OssService : DomainService
    {
        private readonly IConfiguration configuration;

        public OssService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> UploadAsync(byte[] bytes, string fileName)
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
            var newFileName = Guid.NewGuid().ToString("N") + extension;
            var fullPath = Path.Combine(path, newFileName);
            using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fs.Seek(0, SeekOrigin.Begin);
                await fs.WriteAsync(bytes);
                fs.Close();
            }
            return dir + "/" + newFileName;
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
            var path = configuration["Oss:Bucket"];
            if (Directory.Exists(path)) return path;
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "oss");
        }
    }
}