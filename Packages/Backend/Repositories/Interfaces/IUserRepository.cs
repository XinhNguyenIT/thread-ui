using Backend.Enums;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories.Interfaces;

public interface IUserRepository
{
	Task<User> CreateUserAsync(User user, string password, RoleTypeEnum role);
	Task LoginAsync(string email, string password);
}