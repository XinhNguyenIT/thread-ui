using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Exceptions;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly UserContext _userContext;
		private readonly IRoleRepository _roleRepository;
		private readonly IJwtService _jwtService;
		private readonly UserMapper _userMapper;
		private readonly IUnitOfWork _unitOfWork;

		public AuthService(IUserRepository userRepository,
							IRoleRepository roleRepository,
							IJwtService jwtService,
							UserContext userContext,
							IUnitOfWork unitOfWork,
							UserMapper userMapper)
		{
			_userRepository = userRepository;
			_roleRepository = roleRepository;
			_jwtService = jwtService;
			_userContext = userContext;
			_unitOfWork = unitOfWork;
			_userMapper = userMapper;
		}
		public async Task<AuthInternal> Login(LoginRequest item)
		{
			var result = await _userRepository.LoginAsync(item.Email, item.Password);

			var roles = await _roleRepository.GetByUserAsync(result);

			var tokens = await _jwtService.CreateTokenForUser(result, roles);

			var response = _userMapper.ToAuthInternal(result, roles, tokens);

			return response;
		}

		public Task Logout(int userId)
		{
			throw new NotImplementedException();
		}

		public async Task<AuthInternal> Me()
		{
			var userId = _userContext.UserId;

			var currentUser = await _userRepository.GetByIdAsync(userId) ?? throw new BadHttpRequestException("User not found");

			var roles = await _roleRepository.GetByUserAsync(currentUser);

			var tokens = await _jwtService.CreateTokenForUser(currentUser, roles);

			var response = _userMapper.ToAuthInternal(currentUser, roles, tokens);

			return response;
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

			var newUser = _userMapper.ToModel(request);

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var createdUser = await _userRepository.CreateUserAsync(newUser, request.Password);

				await _roleRepository.AddRoleToUser(createdUser, request.Roles);

				var tokens = await _jwtService.CreateTokenForUser(createdUser, request.Roles);

				var response = _userMapper.ToAuthInternal(createdUser, request.Roles, tokens);

				await _unitOfWork.CommitAsync();
				return response;
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				throw;
			}
		}
	}
}