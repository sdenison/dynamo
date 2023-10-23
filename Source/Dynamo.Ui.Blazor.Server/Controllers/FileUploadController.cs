using Dynamo.Business.Shared.Utilities;
using Dynamo.Business.Utilities;
using Microsoft.AspNetCore.Mvc;
using Dynamo.IO.S3.Models;
using Dynamo.IO.S3.Services;

namespace Dynamo.Ui.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private Csla.IDataPortal<BackgroundJob> _dataPortal;
        private IStorageService _storageService;

        public FileUploadController(Csla.IDataPortal<BackgroundJob> dataPortal, IStorageService storageService)
        {
            _dataPortal = dataPortal;
            _storageService = storageService;
        }

        [HttpPost]
        public async Task<ActionResult<BackgroundJob>> PostFile([FromForm] IEnumerable<IFormFile> files)
        {
            var backgroundJob = await _dataPortal.CreateAsync();

            var file = files.First();

            //var fileName = file.

            backgroundJob.FileName = file.FileName.Split('|')[0];
            backgroundJob.JobType = Enum.Parse<JobType>(file.FileName.Split('|')[1]);
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var s3Obj = new S3Object()
            {
                BucketName = "test-dynamo-file-store2",
                InputStream = memoryStream,
                Name = backgroundJob.Id.ToString()
            };
            var result = await _storageService.UploadFileAsync(s3Obj);
            if (result.StatusCode < 200 || result.StatusCode >= 300)
                throw new FileLoadException("Could not upload file to S3 bucket");
            backgroundJob = await backgroundJob.SaveAsync();
            return Ok(backgroundJob);
        }
    }
}
