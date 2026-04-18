using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces;

public interface IAuthService
{
	Task Login(LoginRequest item);
	Task Logout(int userId);
	Task<RegisterResponse> Register(RegisterRequest request);
}