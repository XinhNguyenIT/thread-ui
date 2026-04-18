
using Backend.Dataset.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Dataset;

public class IdentitySeeder
{
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public IdentitySeeder(
		UserManager<User> userManager,
		RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task SeedAsync(ISeedData data)
	{
		await SeedRoles(data);

		await SeedUser(data);
	}

	private async Task SeedRoles(ISeedData data)
	{
		foreach (var role in data.Roles)
		{
			if (!await _roleManager.RoleExistsAsync(role))
			{
				await _roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}

	private async Task SeedUser(ISeedData data)
	{
		foreach (var item in data.Users)
		{
			var existingUser = await _userManager.FindByEmailAsync(item.user.Email);

			if (existingUser != null) continue;

			var result = await _userManager.CreateAsync(item.user, item.password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(item.user, item.role);
			}
		}
	}
}