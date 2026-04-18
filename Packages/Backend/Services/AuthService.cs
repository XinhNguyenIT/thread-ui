using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Exceptions;
using Backend.Mappers;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;

		public AuthService(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
		}
		public async Task Login(LoginRequest item)
		{
			await _userRepository.LoginAsync(item.Email, item.Password);
		}

		public Task Logout(int userId)
		{
			throw new NotImplementedException();
		}

		public async Task<RegisterResponse> Register(RegisterRequest request)
		{
			if (request.FirstName is null && request.LastName is null)
				throw new BadHttpRequestException("Either first name or last name must be provided.");

			var role = await _roleRepository.GetByNameAsync(request.Role.ToString());

			if (role is null) throw new KeyNotFoundException("Role not found!");

			var newUser = UserMapper.ToModel(request);

			var result = await _userRepository.CreateUserAsync(newUser, request.Password, request.Role);

			var response = UserMapper.ToRegisterResponse(result, request.Role);

			return response;
		}
	}
}