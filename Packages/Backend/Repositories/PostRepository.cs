using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories
{
	public class PostRepository : IPostRepository
	{
		private readonly ThreadDbContext _context;

		public PostRepository(ThreadDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Post entity)
		{
			await _context.Posts.AddAsync(entity);
		}

		public void Delete(Post entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Post>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Post?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public List<Post> GetPagedPost(int page = 1, int pageSize = 10)
		{
			throw new NotImplementedException();
		}

		public void Update(Post entity)
		{
			throw new NotImplementedException();
		}
	}
}