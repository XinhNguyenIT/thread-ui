using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces;

public interface IAuthService
{
	Task<AuthInternal> Login(LoginRequest item);
	Task Logout();
	Task Logout(int userId);
	Task<AuthInternal> Register(RegisterRequest request);
	Task<AuthInternal> Me();
	Task<AuthInternal> ByRefreshToken(string refreshToken);


}