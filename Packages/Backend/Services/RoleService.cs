using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
	public class RoleService : IRoleRepository
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
	}
}