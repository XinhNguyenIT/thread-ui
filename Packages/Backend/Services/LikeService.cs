using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.Enums;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class LikeService : ILikeService
	{
		private readonly UserContext _userContext;
		private readonly IUnitOfWork _unitOfWork;

		public LikeService(UserContext userContext, IUnitOfWork unitOfWork)
		{
			_userContext = userContext;
			_unitOfWork = unitOfWork;
		}

		public async Task UserLikeTarget(int targetId, TargetTypeEnum targetType)
		{
			var userId = _userContext.UserId;

			int adjustment = 0;

			var existingLike = await _unitOfWork.LikeRepository
				.GetLikeByUserIdAndTargetId(userId, targetId, targetType);

			if (existingLike != null)
			{
				_unitOfWork.LikeRepository.Delete(existingLike);
				adjustment = -1;
			}
			else
			{
				var like = new Like
				{
					UserId = userId,
					TargetId = targetId,
					TargetType = targetType
				};

				await _unitOfWork.LikeRepository.AddAsync(like);
				adjustment = 1;
			}

			switch (targetType)
			{
				case TargetTypeEnum.POST:
					var post = await _unitOfWork.PostRepository.GetByIdAsync(targetId);
					if (post != null)
					{
						post.LikeCount += adjustment;
					}
					break;
				case TargetTypeEnum.COMMENT:
					var comment = await _unitOfWork.CommentRepository.GetByIdAsync(targetId);
					if (comment != null)
					{
						comment.LikeCount += adjustment;
					}
					break;
				case TargetTypeEnum.STORY:
					var story = await _unitOfWork.StoryRepository.GetByIdAsync(targetId);
					if (story != null)
					{
						story.LikeCount += adjustment;
					}
					break;
			}

			await _unitOfWork.CommitAsync();
		}
	}
}