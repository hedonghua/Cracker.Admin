using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Cracker.Admin.Filters;
using Cracker.Admin.Oss;
using Cracker.Admin.Oss.Dtos;

using Volo.Abp.AspNetCore.Mvc;

namespace Cracker.Admin.Controllers.Oss
{
    public class OssController : AbpControllerBase
    {
        private readonly IOssService _ossService;

        public OssController(IOssService ossService)
        {
            _ossService = ossService;
        }

        [HttpPost]
        [Route("oss/upload")]
        [AppResultFilter]
        public async Task<string> UploadAsync(IFormFile file)
        {
            var stream = file.OpenReadStream();
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            await stream.ReadAsync(bytes);
            var dto = new UploadDto
            {
                FileName = file.FileName,
                Size = (int)file.Length,
                Bytes = bytes
            };
            stream.Dispose();
            stream.Close();
            return await _ossService.UploadAsync(dto);
        }

        [HttpGet]
        [Route(FileManager.PREVIEW_API)]
        public async Task<IActionResult> PreviewAsync(Guid id)
        {
            try
            {
                var dto = await _ossService.GetAsync(id.ToString());
                return File(dto.Bytes, dto.MimeType, dto.FileName);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}