using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class LikeRepository : ILikeRepository
	{
		private readonly ThreadDbContext _context;

		public LikeRepository(ThreadDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Like entity)
		{
			await _context.Likes.AddAsync(entity);
		}

		public async void Delete(Like entity)
		{
			_context.Likes.Remove(entity);
		}

		public Task<IEnumerable<Like>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Like?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<Like?> GetLikeByUserIdAndTargetId(string userId, int targetId, TargetTypeEnum targetType)
		{
			return await _context.Likes
				.FirstOrDefaultAsync(l => l.UserId == userId && l.TargetId == targetId && l.TargetType == targetType);
		}

		public Task Update(Like entity)
		{
			throw new NotImplementedException();
		}

		public Task UserLikeTarget(string userId, int targetId, TargetTypeEnum targetType)
		{
			throw new NotImplementedException();
		}
	}
}