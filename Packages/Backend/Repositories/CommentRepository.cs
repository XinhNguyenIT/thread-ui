using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

		public async Task<List<Comment>> GetCommentByParentCommentIdAsync(int parentCommentId)
		{
			return await _context.Comments
							.AsNoTracking()
							.Where(c => c.ParentCommentId == parentCommentId)
							.Include(c => c.ChildComments)
							.Include(c => c.User)
							.ThenInclude(u => u.Posts.Where(p => p.IsAvatar))
								.ThenInclude(p => p.Medias)
							.ToListAsync();
		}

		public async Task<List<Comment>> GetCommentByPostIdAsync(int postId)
		{
			return await _context.Comments
							.AsNoTracking()
							.Where(c => c.PostId == postId && c.ParentComment == null)
							.Include(c => c.ChildComments)
							.Include(c => c.User)
							.ThenInclude(u => u.Posts.Where(p => p.IsAvatar))
								.ThenInclude(p => p.Medias)
							.ToListAsync();

			// .Select(c => new CommentResponse
			// {
			// 	User = new UserBasicResponse
			// 	{
			// 		UserId = c.UserId,
			// 		FirstName = c.User.FirstName,
			// 		LastName = c.User.LastName,
			// 		Gender = c.User.Gender,
			// 		Avatar = c.User.Posts
			// 			.Where(p => p.IsAvatar)
			// 			.SelectMany(p => p.Medias)
			// 			.Select(m => new MediaResponse
			// 			{
			// 				Src = m.Status == MediaStatusEnum.DONE ? m.ProcessedSrc : m.Src,
			// 				Type = 
			// 			})
			// 			.FirstOrDefault()
			// 							}
			// 						})
			// .ToListAsync();
		}

		public Task Update(Comment entity)
		{
			throw new NotImplementedException();
		}
	}
}