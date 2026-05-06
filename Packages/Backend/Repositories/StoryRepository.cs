using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories
{
	public class StoryRepository : IStoryRepository
	{
		private readonly ThreadDbContext _context;

		public StoryRepository(ThreadDbContext context)
		{
			_context = context;
		}

		public Task AddAsync(Story entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Story entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Story>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Story?> GetByIdAsync(int id)
		{
			return await _context.Stories.FindAsync(id);
		}

		public Task Update(Story entity)
		{
			throw new NotImplementedException();
		}
	}
}