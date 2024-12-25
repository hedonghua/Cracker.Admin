using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Cracker.Admin.Entity;
using Cracker.Admin.Helpers;
using Cracker.Admin.Oss.Dtos;

using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Cracker.Admin.Oss
{
    public class OssService : ApplicationService, IOssService
    {
        private readonly IBlobContainer _blobContainer;
        private readonly IRepository<SysFile> _fileRepository;
        private readonly IConfiguration _configuration;

        public OssService(IBlobContainerFactory blobContainerFactory, IRepository<SysFile> fileRepository, IConfiguration configuration)
        {
            _blobContainer = blobContainerFactory.Create<DefaultContainer>();
            _fileRepository = fileRepository;
            _configuration = configuration;
        }

        public async Task<FileResultDto> GetAsync(string uniqueName)
        {
            var bytes = await _blobContainer.GetAllBytesOrNullAsync(uniqueName) ?? throw new Exception("文件不存在");
            var file = await _fileRepository.SingleOrDefaultAsync(x => x.Id.ToString() == uniqueName || x.UniqueName == uniqueName) ?? throw new Exception("文件读取错误");
            var rs = new FileResultDto
            {
                Bytes = bytes,
                FileName = file.FileName,
                MimeType = file.MimeType
            };
            return rs;
        }

        [UnitOfWork(isTransactional: true)]
        public async Task<string> UploadAsync(UploadDto dto)
        {
            Guid id = Guid.NewGuid();
            var file = new SysFile
            {
                Size = dto.Size,
                Suffix = Path.GetExtension(dto.FileName),
                MimeType = MimeTypesMapHelper.GetMimeType(dto.FileName),
                UniqueName = id.ToString(),
                UploadType = Entity.Enums.UploadType.BlobFileSystem
            };
            file.FileName = Path.GetRandomFileName() + file.Suffix;
            file.RelativeUrl = FileManager.GetRealPreviewApiUrl(id);
            file = await _fileRepository.InsertAsync(file, true);
            await _blobContainer.SaveAsync(file.UniqueName, dto.Bytes);
            return UriHelper.Concat(false, _configuration["App:Domain"], file.RelativeUrl);
        }
    }
}