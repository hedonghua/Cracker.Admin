using Cracker.Admin.Filters;
using Cracker.Admin.Helpers;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Oss
{
    public class OssController : AbpControllerBase
    {
        private readonly OssService _ossService;

        public OssController(OssService ossService)
        {
            _ossService = ossService;
        }

        [HttpPost]
        [AppResultFilter]
        public async Task<string> UploadAsync(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            await stream.ReadAsync(bytes);
            stream.Dispose();
            stream.Close();
            return await _ossService.UploadAsync(bytes, file.FileName);
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> PreviewAsync(string fileName)
        {
            try
            {
                var bytes = await _ossService.GetFileAsync(fileName);
                var name = Path.GetFileName(fileName);
                var mimeType = MimeTypesMapHelper.GetMimeType(name);
                return File(bytes, mimeType, name);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}