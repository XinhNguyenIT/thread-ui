using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Exceptions;
using Backend.Mappers;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly IJwtService _jwtService;

		public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, IJwtService jwtService)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_jwtService = jwtService;
		}
		public async Task<AuthInternal> Login(LoginRequest item)
		{
			var result = await _userRepository.LoginAsync(item.Email, item.Password);

			var roles = await _roleRepository.GetByUserAsync(result);

			var tokens = await _jwtService.CreateTokenForUser(result, roles);

			var response = UserMapper.ToAuthInternal(result, roles, tokens);

			return response;
		}

		public Task Logout(int userId)
		{
			throw new NotImplementedException();
		}

		public async Task<AuthInternal> Register(RegisterRequest request)
		{
			if (request.FirstName is null && request.LastName is null)
				throw new BadHttpRequestException("Either first name or last name must be provided.");

			var existRoles = await _roleRepository.GetAllAsync();

			if (!existRoles.Any(r => request.Roles.Any(reqR => reqR.ToString() == r.Name)))
			{
				throw new BadHttpRequestException("Role not found");
			}

			var newUser = UserMapper.ToModel(request);

			var result = await _userRepository.CreateUserAsync(newUser, request.Password, request.Roles);

			var tokens = await _jwtService.CreateTokenForUser(result, request.Roles);

			var response = UserMapper.ToAuthInternal(result, request.Roles, tokens);

			return response;
		}
	}
}