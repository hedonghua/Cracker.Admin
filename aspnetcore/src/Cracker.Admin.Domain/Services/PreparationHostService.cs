using Cracker.Admin.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Cracker.Admin.Services
{
    public class PreparationHostService : BackgroundService
    {
        private readonly OssService ossService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public PreparationHostService(OssService ossService, IWebHostEnvironment webHostEnvironment)
        {
            this.ossService = ossService;
            this.webHostEnvironment = webHostEnvironment;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //默认头像
            await SaveDefaultAvatarAsync(stoppingToken);
            //保存RSA公私钥
            await SaveRSAKeysAsync();
        }

        private static async Task SaveRSAKeysAsync()
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RSAKeys");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var publicKeysPath = Path.Combine(dir, "PublicKeys.txt");
            var privateKeysPath = Path.Combine(dir, "PrivateKeys.txt");
            if (!File.Exists(publicKeysPath) || !File.Exists(privateKeysPath))
            {
                var (publicKey, privateKey) = EncryptionHelper.GenerateRSAKeys();
                await File.WriteAllTextAsync(publicKeysPath, publicKey);
                await File.WriteAllTextAsync(privateKeysPath, privateKey);
            }
        }

        private async Task SaveDefaultAvatarAsync(CancellationToken stoppingToken)
        {
            var dir = Path.Combine(webHostEnvironment.WebRootPath, "avatar");
            var files = Directory.GetFiles(dir);
            if (files.Length > 0)
            {
                //默认头像写入oss
                foreach (var item in files)
                {
                    var fileName = Path.GetFileName(item);
                    using var fs = new FileStream(item, FileMode.Open, FileAccess.Read);
                    var bytes = new byte[fs.Length];
                    fs.Seek(0, SeekOrigin.Begin);
                    await fs.ReadAsync(bytes, stoppingToken);
                    await ossService.UploadAsync(bytes, "avatar/" + fileName, false);
                    fs.Close();
                }
            }
        }
    }
}