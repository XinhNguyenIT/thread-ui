using System.Threading.Tasks;
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

		public Task<IEnumerable<User>> GetAllAsync()
		{
			throw new NotImplementedException();
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

		public Task<User?> GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<User?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(User entity)
		{
			throw new NotImplementedException();
		}

		public async Task Update(User entity)
		{
			var currentUser = await _userManager.FindByIdAsync(entity.Id.ToString());

			if (currentUser == null)
			{
				throw new KeyNotFoundException("User not found");
			}

			currentUser.FirstName = entity.FirstName;
			currentUser.Gender = entity.Gender;
			currentUser.LastName = entity.LastName;

			var result = await _userManager.UpdateAsync(currentUser);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new ValidationException("Update user failed", errors);
			}
		}

		public void Delete(User entity)
		{
			throw new NotImplementedException();
		}
	}
}