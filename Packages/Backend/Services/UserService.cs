using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Background.Queue;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Helpers;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly PostMapper _postMapper;
		private readonly UserMapper _userMapper;
		private readonly UserContext _userContext;
		private readonly IFileService _fileService;
		private readonly IMediaQueue _mediaQueue;

		public UserService(IUnitOfWork unitOfWork, PostMapper postMapper, UserContext userContext, IFileService fileService, UserMapper userMapper, IMediaQueue mediaQueue)
		{
			_unitOfWork = unitOfWork;
			_postMapper = postMapper;
			_userContext = userContext;
			_fileService = fileService;
			_userMapper = userMapper;
			_mediaQueue = mediaQueue;
		}

		public async Task<UpdateUserResponse> Update(UpdateUserRequest request)
		{
			var newUser = _userMapper.ToModel(request);
			var userId = _userContext.UserId;
			newUser.Id = userId;

			await _unitOfWork.UserRepository.Update(newUser);
			await _unitOfWork.SaveChangesAsync();

			var response = _userMapper.ToUpdateUserResponse(newUser);
			return response;
		}

		public async Task<PostResponse> UpdateAvatar(UpdateAvatarRequest request)
		{
			var typeMedia = TypeMediaHelper.Get(request.File);
			if (typeMedia != MediaTypeEnum.IMAGE)
				throw new BadHttpRequestException("Avatar must be image!");

			var userId = _userContext.UserId;
			var fileName = TypeMediaHelper.GetFileName(request.File);

			var media = new Media
			{
				Src = fileName,
				Type = TypeMediaHelper.Get(request.File)
			};

			await _unitOfWork.BeginTransactionAsync();

			var finalName = "";

			try
			{
				finalName = await _fileService.MoveToPermanentAsync(fileName);

				media.Src = finalName;

				var newPost = _postMapper.ToModel(request, userId, media);
				await _unitOfWork.PostRepository.DisableAllAvatarsAsync(userId);

				await _unitOfWork.PostRepository.AddAsync(newPost);
				await _unitOfWork.CommitAsync();

				_mediaQueue.Enqueue(media.MediaId);

				var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
				var avatar = await _unitOfWork.MediaRepository.GetAvtSrcByUserId(userId);
				var response = _postMapper.ToPostResponse(newPost, user, avatar);

				return response;
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				await _fileService.DeleteAsync(finalName);
				throw;
			}
		}
	}
}