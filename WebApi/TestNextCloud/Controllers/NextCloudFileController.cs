using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace TestOwnCloudConnect.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class NextCloudFileController : ControllerBase
    {

        private readonly INextCloudService _nextCloudService;


        public NextCloudFileController(INextCloudService nextCloudService)
        {
            _nextCloudService = nextCloudService;
        }


        [HttpGet("GetFileList")]
        public async Task<IActionResult> GetFileList()
        {
            try
            {
                var response = await _nextCloudService.GetFileList();
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            try
            {
                var response = await _nextCloudService.DownloadFile(filename);
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm]  IFormFile file)
        {
            try
            {
                var response = await _nextCloudService.UploadFile(file);
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }

        
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var response = await _nextCloudService.UploadFiles(files);
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }

        
        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            try
            {
                //Upload file to /Documents/ - that is a default folder in NextCloud.
                var response = await _nextCloudService.UploadFile(file, "/Documents/");
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpPost("UploadDocuments")]
        public async Task<IActionResult> UploadDocuments(List<IFormFile> files)
        {
            try
            {
                //Upload file to /Documents/ - that is a default folder in NextCloud.
                var response = await _nextCloudService.UploadFiles(files, "/Documents/");
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpPost("UploadPhoto")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            try
            {
                //Upload file to /Photos/ - that is a default folder in NextCloud.
                var response = await _nextCloudService.UploadFile(file, "/Photos/");
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpPost("UploadPhotos")]
        public async Task<IActionResult> UploadPhotos(List<IFormFile> files)
        {
            try
            {
                //Upload file to /Photos/ - that is a default folder in NextCloud.
                var response = await _nextCloudService.UploadFiles(files, "/Photos/");
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpPost("UploadTemplate")]
        public async Task<IActionResult> UploadTemplate(IFormFile file)
        {
            try
            {
                //Upload file to /Template/ - that is a default folder in NextCloud.
                var response = await _nextCloudService.UploadFile(file, "/Template/");
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }
        

        [HttpPost("UploadTemplates")]
        public async Task<IActionResult> UploadTemplates(List<IFormFile> files)
        {
            try
            {
                //Upload file to /Template/ - that is a default folder in NextCloud.
                var response = await _nextCloudService.UploadFiles(files, "/Template/");
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpDelete("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string filename)
        {
            try
            {
                var response = await _nextCloudService.DeleteFile(filename);
                return Ok(response);
            }
            catch
            {
                return BadRequest("Something went wrong.");
            }
        }


    }

}
