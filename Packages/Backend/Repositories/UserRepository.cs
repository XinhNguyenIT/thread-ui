using Backend.Enums;
using Backend.Exceptions;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;

		private readonly IUnitOfWork _unitOfWork;

		public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_unitOfWork = unitOfWork;
		}

		public async Task<User> CreateUserAsync(User user, string password, List<RoleTypeEnum> roles)
		{
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				var result = await _userManager.CreateAsync(user, password);

				if (!result.Succeeded)
				{
					throw new ValidationException(
						"Register failed",
						result.Errors.Select(e => e.Description).ToList()
					);
				}

				var roleNames = roles.Select(r => r.ToString()).ToList();
				var roleResult = await _userManager.AddToRolesAsync(user, roleNames);

				if (!roleResult.Succeeded) throw new BadHttpRequestException("Failed to assign roles.");

				await _unitOfWork.CommitAsync();
				return user;
			}
			catch (Exception)
			{
				await _unitOfWork.RollbackAsync();
				throw;
			}
		}

		public async Task<User> LoginAsync(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email) ?? throw new UnauthorizedAccessException("Invalid email");
			var result = await _signInManager.CheckPasswordSignInAsync(
				user,
				password,
				lockoutOnFailure: false
			);

			if (!result.Succeeded)
				throw new UnauthorizedAccessException("Invalid email or password");

			return user;
		}
	}
}