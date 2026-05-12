using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class CommentService : ICommentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserMapper _userMapper;
		private readonly UserContext _userContext;

		public CommentService(IUnitOfWork unitOfWork, UserMapper userMapper, UserContext userContext)
		{
			_unitOfWork = unitOfWork;
			_userMapper = userMapper;
			_userContext = userContext;
		}

		public async Task<CommentResponse> CreateCommentAsync(CreateCommentRequest request)
		{
			if (request.CommentId is null && request.PostId is null)
			{
				throw new BadHttpRequestException("CommentId or PostId is required");
			}

			if (request.CommentId is not null && request.PostId is not null)
			{
				throw new BadHttpRequestException("Only one parameter is allowed");
			}

			var userId = _userContext.UserId;

			var comment = new Comment
			{
				UserId = userId,
				Content = request.content,
				CommentId = request.CommentId.Value,
				PostId = request.PostId.Value,
			};

			await _unitOfWork.CommentRepository.AddAsync(comment);
			await _unitOfWork.SaveChangesAsync();

			var response = new CommentResponse
			{
				User = _userMapper.ToUserBasicResponse(comment.User, comment.User.Posts.FirstOrDefault().Medias.FirstOrDefault()),
				Content = comment.Content,
				ChildCommentCount = comment.ChildComments.Count(),
				LikeCount = comment.LikeCount,
				ParentId = comment.ParentCommentId.Value
			};

			return response;
		}

		public async Task<List<CommentResponse>> GetCommentAsync(GetCommentRequest request)
		{
			if (request.CommentId is null && request.PostId is null)
			{
				throw new BadHttpRequestException("CommentId or PostId is required");
			}

			if (request.CommentId is not null && request.PostId is not null)
			{
				throw new BadHttpRequestException("Only one parameter is allowed");
			}

			var comments = new List<Comment>();

			if (request.CommentId is not null)
			{
				comments = await _unitOfWork.CommentRepository.GetCommentByParentCommentIdAsync(request.CommentId.Value);
			}
			else
			{
				comments = await _unitOfWork.CommentRepository.GetCommentByPostIdAsync(request.PostId!.Value);
			}

			var response = comments.Select(c => new CommentResponse
			{
				User = _userMapper.ToUserBasicResponse(c.User, c.User.Posts.FirstOrDefault().Medias.FirstOrDefault()),
				Content = c.Content,
				ChildCommentCount = c.ChildComments.Count(),
				LikeCount = c.LikeCount,
				ParentId = c.ParentCommentId.Value
			}).ToList();

			return response;
		}
	}
}