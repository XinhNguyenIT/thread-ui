using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Backend.Common;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Helpers;
using Backend.Mappers;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request);

            foreach (var token in result.Tokens)
            {
                var cookieName = token.Type == TokenTypeEnum.ACCESS ? "access" : "refresh";
                CookieHelper.SetSecureCookie(Response, cookieName, token.Token, token.Expires);
            }

            var response = UserMapper.ToAuthResponse(result);

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Registration successful"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request);

            foreach (var token in result.Tokens)
            {
                var cookieName = token.Type == TokenTypeEnum.ACCESS ? "access" : "refresh";
                CookieHelper.SetSecureCookie(Response, cookieName, token.Token, token.Expires);
            }

            var response = UserMapper.ToAuthResponse(result);

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Login success"));
        }
    }
}