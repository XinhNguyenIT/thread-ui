using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repositories.Interfaces
{
	public interface IRoleRepository
	{
		Task<bool> CreateRoleAsync(string roleName);
		Task<bool> DeleteRoleAsync(string roleName);
		Task<IdentityRole?> GetByIdAsync(string roleId);
		Task<IdentityRole?> GetByNameAsync(string roleName);
		Task<List<RoleTypeEnum>> GetByUserAsync(User user);
		Task<List<IdentityRole>> GetAllAsync();
		Task AddRoleToUser(User user, List<RoleTypeEnum> roles);

	}
}