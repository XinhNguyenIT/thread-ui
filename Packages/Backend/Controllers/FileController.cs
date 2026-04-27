using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMediaService _mediaService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload-temp")]
        public async Task<IActionResult> UploadTemp([FromForm] List<IFormFile> files)
        {
            var result = new List<string>();

            foreach (var file in files)
            {
                var tempName = await _fileService.SaveTempAsync(file);
                result.Add(tempName);
            }

            return Ok(result);
        }
    }
}