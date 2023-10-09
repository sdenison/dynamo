using Dynamo.IO.S3.Models;

namespace Dynamo.IO.S3.Services
{
    public interface IStorageService
    {
        Task<S3ResponseDto> UploadFileAsync(S3Object obj);
        Task<Stream> DownloadFileAsync(S3Object obj);
    }
}
