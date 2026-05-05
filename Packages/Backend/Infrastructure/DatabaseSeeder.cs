using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Dataset;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure
{
	public interface IDatabaseSeeder
	{
		Task SeedAsync(string? type);
	}

	public class DatabaseSeeder : IDatabaseSeeder
	{
		private readonly IdentitySeeder _identitySeeder;
		private readonly ThreadDbContext _db;
		private readonly SeedDataFactory _factory;

		public DatabaseSeeder(IdentitySeeder identitySeeder, ThreadDbContext db, SeedDataFactory factory)
		{
			_identitySeeder = identitySeeder;
			_db = db;
			_factory = factory;
		}

		public async Task SeedAsync(string? type)
		{
			// reset DB
			await ClearDataAsync();

			var data = _factory.Get(type);

			// seed identity
			await _identitySeeder.SeedAsync(data);

			// seed posts
			foreach (var post in data.Posts)
			{
				_db.Posts.Add(post);
			}
			await _db.SaveChangesAsync();
		}

		public async Task ClearDataAsync()
		{
			// reset DB
			await _db.Database.EnsureDeletedAsync();
			await _db.Database.MigrateAsync();
		}
	}
}