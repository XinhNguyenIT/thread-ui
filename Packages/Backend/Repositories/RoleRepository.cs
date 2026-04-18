using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleRepository(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<bool> CreateRoleAsync(string roleName)
		{
			var roleExist = await _roleManager.RoleExistsAsync(roleName);
			if (roleExist) return false;

			var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
			return result.Succeeded;
		}

		public async Task<bool> DeleteRoleAsync(string roleName)
		{
			var role = await _roleManager.FindByNameAsync(roleName);
			if (role == null) return false;

			var result = await _roleManager.DeleteAsync(role);
			return result.Succeeded;
		}

		public async Task<List<IdentityRole>> GetAllAsync()
		{
			return await _roleManager.Roles.ToListAsync();
		}

		public async Task<IdentityRole?> GetByIdAsync(string roleId)
		{
			return await _roleManager.FindByIdAsync(roleId);
		}

		public async Task<IdentityRole?> GetByNameAsync(string roleName)
		{
			return await _roleManager.FindByNameAsync(roleName);
		}
	}
}