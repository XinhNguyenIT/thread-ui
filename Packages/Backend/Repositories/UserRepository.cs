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

		public Task AddAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public async Task<User> CreateUserAsync(User user, string password)
		{
			var result = await _userManager.CreateAsync(user, password);

			if (!result.Succeeded)
			{
				throw new ValidationException(
					"Register failed",
					result.Errors.Select(e => e.Description).ToList()
				);
			}

			return user;
		}

		public async Task<User?> GetByIdAsync(string id)
		{
			return await _userManager.FindByIdAsync(id);
		}

		public async Task<User> LoginAsync(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email) ?? throw new BadHttpRequestException("Invalid email");
			var result = await _signInManager.CheckPasswordSignInAsync(
				user,
				password,
				lockoutOnFailure: false
			);

			if (!result.Succeeded)
				throw new BadHttpRequestException("Invalid email or password");

			return user;
		}
	}
}