using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Cracker.Admin.Services
{
    public class StaticFileHostService : BackgroundService
    {
        private readonly OssService ossService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StaticFileHostService(OssService ossService, IWebHostEnvironment webHostEnvironment)
        {
            this.ossService = ossService;
            this.webHostEnvironment = webHostEnvironment;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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