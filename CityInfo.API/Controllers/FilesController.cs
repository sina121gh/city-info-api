using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        #region Dependency Injection

        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        #endregion

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            string filePath = "WebApiBanner.jpg";

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            if (!_fileExtensionContentTypeProvider.TryGetContentType(filePath, out string contentType))
                contentType = "application/octet-stream";

            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
