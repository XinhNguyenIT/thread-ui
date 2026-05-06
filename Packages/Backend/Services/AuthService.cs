using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.Mappers;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserContext _userContext;
		private readonly IJwtService _jwtService;
		private readonly UserMapper _userMapper;
		private readonly IUnitOfWork _unitOfWork;

		public AuthService(IJwtService jwtService,
							UserContext userContext,
							IUnitOfWork unitOfWork,
							UserMapper userMapper)
		{
			_jwtService = jwtService;
			_userContext = userContext;
			_unitOfWork = unitOfWork;
			_userMapper = userMapper;
		}
		public async Task<AuthInternal> Login(LoginRequest item)
		{
			var result = await _unitOfWork.UserRepository.LoginAsync(item.Email, item.Password);

			var roles = await _unitOfWork.RoleRepository.GetByUserAsync(result);

			var tokens = await _jwtService.CreateTokenForUser(result, roles);

			var avtSrc = await _unitOfWork.MediaRepository.GetAvtSrcByUserId(result.Id);

			var response = _userMapper.ToAuthInternal(result, roles, tokens, avtSrc);

			return response;
		}

		public Task Logout(int userId)
		{
			throw new NotImplementedException();
		}

		public async Task<AuthInternal> Me()
		{
			var userId = _userContext.UserId;
			var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(userId) ?? throw new BadHttpRequestException("User not found");

			var roles = await _unitOfWork.RoleRepository.GetByUserAsync(currentUser);

			var tokens = await _jwtService.CreateTokenForUser(currentUser, roles);

			var avtSrc = await _unitOfWork.MediaRepository.GetAvtSrcByUserId(currentUser.Id);

			var response = _userMapper.ToAuthInternal(currentUser, roles, tokens, avtSrc);

			return response;
		}

		public async Task<AuthInternal> ByRefreshToken(string refreshToken)
		{
			var currentUser = await _unitOfWork.UserRepository.GetByRefreshTokenAsync(refreshToken) ?? throw new BadHttpRequestException("User not found");

			var roles = await _unitOfWork.RoleRepository.GetByUserAsync(currentUser);

			var isCreateRefreshToken = false;

			var tokens = await _jwtService.CreateTokenForUser(currentUser, roles, isCreateRefreshToken);

			var avtSrc = await _unitOfWork.MediaRepository.GetAvtSrcByUserId(currentUser.Id);

			var response = _userMapper.ToAuthInternal(currentUser, roles, tokens, avtSrc);

			return response;
		}

		public async Task<AuthInternal> Register(RegisterRequest request)
		{
			if (request.FirstName is null && request.LastName is null)
				throw new BadHttpRequestException("Either first name or last name must be provided.");

			var existRoles = await _unitOfWork.RoleRepository.GetAllAsync();

			if (!existRoles.Any(r => request.Roles.Any(reqR => reqR.ToString() == r.Name)))
			{
				throw new BadHttpRequestException("Role not found");
			}

			var newUser = _userMapper.ToModel(request);

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var createdUser = await _unitOfWork.UserRepository.CreateUserAsync(newUser, request.Password);

				await _unitOfWork.RoleRepository.AddRoleToUser(createdUser, request.Roles);

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