using System.Threading.Tasks;

using Cracker.Admin.Oss.Dtos;

using Volo.Abp.Application.Services;

namespace Cracker.Admin.Oss
{
    public interface IOssService : IApplicationService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>文件地址</returns>
        Task<string> UploadAsync(UploadDto dto);


        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="uniqueName">唯一文件名</param>
        /// <returns></returns>
        Task<FileResultDto> GetAsync(string uniqueName);
    }
}