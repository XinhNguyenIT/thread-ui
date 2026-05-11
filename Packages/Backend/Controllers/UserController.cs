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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("me/avatar")]
        public async Task<IActionResult> UpdatePictureAsync(UpdateAvatarRequest request)
        {
            var response = await _userService.UpdateAvatar(request);

            return Ok(ApiResponse<UpdateAvatarResponse>.SuccessResponse(response, "Avatar updated successfully"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            var response = await _userService.Update(request);

            return Ok(ApiResponse<UpdateUserResponse>.SuccessResponse(response, "Profile updated successfully"));
        }
    }
}