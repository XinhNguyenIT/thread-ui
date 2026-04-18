using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services.Interfaces
{
	public interface IRoleService
	{
		Task<bool> CreateRoleAsync(string roleName);
		Task<bool> DeleteRoleAsync(string roleName);
		Task<IdentityRole?> GetByIdAsync(string roleId);
		Task<IdentityRole?> GetByNameAsync(string roleName);
		Task<List<IdentityRole>> GetAllAsync();
	}
}