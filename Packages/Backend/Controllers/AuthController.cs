using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Backend.Common;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Helpers;
using Backend.Mappers;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserMapper _userMapper;

        public AuthController(IAuthService authService, UserMapper userMapper)
        {
            _authService = authService;
            _userMapper = userMapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request);

            SetTokensInsideCookies(result.Tokens);

            var response = _userMapper.ToAuthResponse(result);

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Registration successful"));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var result = await _authService.Me();

            SetTokensInsideCookies(result.Tokens);

            var response = _userMapper.ToAuthResponse(result);

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.Login(request);

            SetTokensInsideCookies(result.Tokens);

            var response = _userMapper.ToAuthResponse(result);

            return Ok(ApiResponse<AuthResponse>.SuccessResponse(response, "Login success"));
        }

        private void SetTokensInsideCookies(IEnumerable<TokenReturn> tokens)
        {
            foreach (var token in tokens)
            {
                var cookieName = token.Type == TokenTypeEnum.ACCESS ? "access" : "refresh";
                CookieHelper.SetSecureCookie(Response, cookieName, token.Token, token.Expires);
            }
        }
    }
}