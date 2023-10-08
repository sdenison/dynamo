using Microsoft.AspNetCore.Mvc;

namespace Dynamo.Ui.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PostFile(
            [FromForm] IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                var x = file.Name;
                return Ok();
            }

            return Ok();
        }
    }
}
