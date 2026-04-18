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


		public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public async Task<User> CreateUserAsync(User user, string password, RoleTypeEnum role)
		{
			var result = await _userManager.CreateAsync(user, password);

			if (!result.Succeeded)
			{
				throw new ValidationException(
					"Register failed",
					result.Errors.Select(e => e.Description).ToList()
				);
			}

			var roleName = role.ToString();

			await _userManager.AddToRoleAsync(user, roleName);

			return user;
		}

		public async Task LoginAsync(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email) ?? throw new UnauthorizedAccessException("Invalid email");
			var result = await _signInManager.CheckPasswordSignInAsync(
				user,
				password,
				lockoutOnFailure: false
			);

			if (!result.Succeeded)
				throw new UnauthorizedAccessException("Invalid email or password");
		}
	}
}