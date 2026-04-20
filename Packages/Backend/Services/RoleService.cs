using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
	public class RoleService : IRoleService
	{
		private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		public async Task<bool> CreateRoleAsync(string roleName)
		{
			return await _roleRepository.CreateRoleAsync(roleName);
		}

		public Task<bool> DeleteRoleAsync(string roleName)
		{
			throw new NotImplementedException();
		}

		public Task<List<IdentityRole>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IdentityRole?> GetByIdAsync(string roleId)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityRole?> GetByNameAsync(string roleName)
		{
			throw new NotImplementedException();
		}

		public async Task<List<RoleTypeEnum>> GetByUserAsync(User user)
		{
			return await _roleRepository.GetByUserAsync(user);
		}
	}
}