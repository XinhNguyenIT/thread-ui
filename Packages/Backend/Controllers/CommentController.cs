using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Common;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetCommentRequest request)
        {
            var response = await _commentService.GetCommentAsync(request);

            return Ok(ApiResponse<List<CommentResponse>>.SuccessResponse(response, "Get comments successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateCommentRequest request)
        {
            var response = await _commentService.CreateCommentAsync(request);

            return Ok(ApiResponse<CommentResponse>.SuccessResponse(response, "Create comment successfully"));
        }
    }
}