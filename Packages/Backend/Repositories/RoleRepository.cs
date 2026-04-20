using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class RoleRepository : IRoleRepository
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<User> _userManager;


		public RoleRepository(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public async Task AddRoleToUser(User user, List<RoleTypeEnum> roles)
		{
			var roleNames = roles.Select(r => r.ToString()).ToList();
			var roleResult = await _userManager.AddToRolesAsync(user, roleNames);

			if (!roleResult.Succeeded) throw new BadHttpRequestException("Failed to assign roles.");
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

		public async Task<List<RoleTypeEnum>> GetByUserAsync(User user)
		{
			var roles = await _userManager.GetRolesAsync(user);

			return roles
				.Select(r => Enum.TryParse<RoleTypeEnum>(r, ignoreCase: true, out var role) ? role : (RoleTypeEnum?)null)
				.Where(r => r.HasValue)
				.Select(r => r!.Value)
				.ToList();
		}
	}
}