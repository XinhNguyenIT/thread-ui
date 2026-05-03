using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class MediaRepository : IMediaRepository
	{
		private readonly ThreadDbContext _context;

		public MediaRepository(ThreadDbContext context)
		{
			_context = context;
		}

		public Task AddAsync(Media entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Media entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Media>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Media?> GetAvtSrcByUserId(string userId)
		{
			return await _context.Medias
							.Where(m => m.Post.UserId == userId && m.Post.IsAvatar)
							.FirstOrDefaultAsync();
		}

		public async Task<Media?> GetByIdAsync(int id)
		{
			return await _context.Medias.FindAsync(id);
		}

		public Task Update(Media entity)
		{
			throw new NotImplementedException();
		}
	}
}