using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Common;
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

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload-temp")]
        public async Task<IActionResult> UploadTemp(IFormFile file)
        {
            var tempName = await _fileService.SaveTempAsync(file);

            return Ok(ApiResponse<string>.SuccessResponse(tempName, "Uploaded successfully"));
        }

        [HttpDelete("delete-temp")]
        public async Task<IActionResult> DeleteTemp([FromQuery] string fullPath)
        {

            await _fileService.DeleteAsync(fullPath);

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Deleted successfully"));
        }
    }
}