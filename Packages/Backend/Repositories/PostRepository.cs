using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.DTOs.Responses;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class PostRepository : IPostRepository
	{
		private readonly ThreadDbContext _context;
		private readonly PostMapper _postMapper;

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

		public async Task DisableAllAvatarsAsync(string userId)
		{
			await _context.Posts
					.Where(p => p.UserId == userId && p.IsAvatar == true)
					.ExecuteUpdateAsync(setters => setters
						.SetProperty(p => p.IsAvatar, false));
		}

		public Task<IEnumerable<Post>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Post?> GetByIdAsync(int id)
		{
			return await _context.Posts.FindAsync(id);
		}

		public async Task<List<PostResponse>> GetPagedPost(string userId, int page = 1, int pageSize = 10)
		{
			if (page < 1) page = 1;
			if (pageSize < 1) pageSize = 10;

			int skipCount = (page - 1) * pageSize;

			var friendIds = _context.Friendships
				.Where(f => f.IsFriend == true)
				.Where(f => f.FromUserId == userId || f.ToUserId == userId)
				.Select(f => f.FromUserId == userId ? f.ToUserId : f.FromUserId);

			return await _context.Posts
				.AsNoTracking()
				.Where(p => p.PrivacySetting != Enums.PrivacySettingEnum.PRIVATE ||
						p.UserId == userId ||
						(p.PrivacySetting == Enums.PrivacySettingEnum.FRIEND && friendIds.Contains(p.UserId)))
				.OrderByDescending(p => p.CreateAt)
				.Skip(skipCount)
				.Take(pageSize)
				.Select(p => new PostResponse
				{
					PostId = p.PostId,
					Author = new UserBasicResponse
					{
						UserId = p.UserId,
						LastName = p.Author.LastName,
						FirstName = p.Author.FirstName,
						Gender = p.Author.Gender,
						Avatar = p.Author.Posts
							.Where(ph => ph.IsAvatar)
							.SelectMany(ph => ph.Medias)
							.Select(m => new MediaResponse
							{
								Id = m.MediaId,
								Type = m.Type,
								Src = m.Src
							})
							.FirstOrDefault()
					},
					Caption = p.Content,
					PrivacySetting = p.PrivacySetting,
					LikesCount = p.Likes.Count(),
					CommentsCount = p.Comments.Count(),
					CreateAt = p.CreateAt,
					IsAvatar = p.IsAvatar,
					Medias = p.Medias.Select(m => new MediaResponse
					{
						Id = m.MediaId,
						Type = m.Type,
						Src = m.Src
					}).ToList(),
				})
				.ToListAsync();
		}

		public Task Update(Post entity)
		{
			throw new NotImplementedException();
		}
	}
}