using Cracker.Admin.Helpers;
using Cracker.Admin.Models;
using Cracker.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Oss
{
    [Route("api/[controller]/[action]")]
    public class OssController : AbpControllerBase
    {
        private readonly OssService _ossService;

        public OssController(OssService ossService)
        {
            _ossService = ossService;
        }

        [HttpPost]
        [Authorize]
        public async Task<AppResponse<string>> UploadAsync(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            await stream.ReadAsync(bytes);
            stream.Dispose();
            stream.Close();

            var fileName = file.FileName;
            if (HttpContext.Request.Headers.TryGetValue("dir", out var dir) && !string.IsNullOrWhiteSpace(dir))
            {
                fileName = dir + "/" + fileName;
            }
            var url = await _ossService.UploadAsync(bytes, fileName);
            return AppResponse.Data(url);
        }

        [HttpGet]
        [Route("/file/{*fileName}")]
        public async Task<IActionResult> ImageAsync([FromRoute] string fileName)
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