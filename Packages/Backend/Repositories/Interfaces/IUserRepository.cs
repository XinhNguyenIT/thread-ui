using Backend.Enums;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories.Interfaces;

public interface IUserRepository
{
	Task<User> CreateUserAsync(User user, string password, List<RoleTypeEnum> roles);
	Task<User> LoginAsync(string email, string password);
}