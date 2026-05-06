using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories.Interfaces;

namespace Backend.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly ThreadDbContext _context;

		public CommentRepository(ThreadDbContext context)
		{
			_context = context;
		}

		public Task AddAsync(Comment entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Comment entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Comment>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Comment?> GetByIdAsync(int id)
		{
			return await _context.Comments.FindAsync(id);
		}

		public Task Update(Comment entity)
		{
			throw new NotImplementedException();
		}
	}
}