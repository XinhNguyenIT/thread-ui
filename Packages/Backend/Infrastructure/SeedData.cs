
using Backend.Dataset.Interfaces;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dataset;

public class IdentitySeeder
{
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly ThreadDbContext _context;

	public IdentitySeeder(
		UserManager<User> userManager,
		RoleManager<IdentityRole> roleManager,
		ThreadDbContext context)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_context = context;
	}

	public async Task SeedAsync(ISeedData data)
	{
		await SeedRoles(data);

		await SeedUser(data);

		await SeedPosts(data);
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

	private async Task SeedPosts(ISeedData data)
	{
		var existingPosts = await _context.Posts.ToListAsync();

		foreach (var post in data.Posts)
		{
			var existingPost = existingPosts.FirstOrDefault(p => p.PostId == post.PostId);
			if (existingPost == null)
			{
				await _context.Posts.AddAsync(post);
			}
		}

		await _context.SaveChangesAsync();
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
				foreach (var role in item.roles)
				{
					await _userManager.AddToRoleAsync(item.user, role.ToString());
				}
			}
		}
	}
}