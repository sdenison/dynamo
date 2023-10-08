using Dynamo.Business.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Dynamo.Ui.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private Csla.IDataPortal<BackgroundJob> _dataPortal;
        public FileUploadController(Csla.IDataPortal<BackgroundJob> dataPortal)
        {
            _dataPortal = dataPortal;
        }

        [HttpPost]
        public async Task<ActionResult<BackgroundJob>> PostFile([FromForm] IEnumerable<IFormFile> files)
        {
            var backgroundJob = await _dataPortal.CreateAsync();

            foreach (var file in files)
            {
                backgroundJob.FileName = file.FileName;
                backgroundJob = await backgroundJob.SaveAsync();
                return Ok(backgroundJob);
            }

            return Ok(null);
        }
    }
}
