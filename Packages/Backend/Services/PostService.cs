using Backend.Background.Queue;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Helpers;
using Backend.Mappers;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;

namespace Backend.Services
{
	public class PostService : IPostService
	{
		private readonly UserContext _userContext;
		private readonly PostMapper _postMapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IFileService _fileService;
		private readonly IMediaQueue _mediaQueue;

		public PostService(UserContext userContext, PostMapper postMapper, IUnitOfWork unitOfWork, IFileService fileService, IMediaQueue mediaQueue)
		{
			_userContext = userContext;
			_postMapper = postMapper;
			_unitOfWork = unitOfWork;
			_fileService = fileService;
			_mediaQueue = mediaQueue;
		}

		public async Task<PostResponse> CreatePost(CreatePostRequest request)
		{
			var userId = _userContext.UserId;
			var medias = new List<Media>();
			var movedFiles = new List<string>();

			foreach (var temp in request.Files)
			{
				var finalName = await _fileService.MoveToPermanentAsync(temp);
				movedFiles.Add(finalName);

				medias.Add(new Media
				{
					Src = finalName,
					Type = TypeMediaHelper.Get(finalName),
				});
			}

			var post = _postMapper.ToModel(request, userId, medias);

			try
			{
				await _unitOfWork.PostRepository.AddAsync(post);
				await _unitOfWork.CommitAsync();

			}
			catch (System.Exception ex)
			{
				foreach (var file in movedFiles)
				{
					await _fileService.DeleteAsync(file);
				}
				throw;
			}


			foreach (var m in medias)
			{
				_mediaQueue.Enqueue(m.MediaId);
			}

			var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
			var avt = await _unitOfWork.MediaRepository.GetAvtSrcByUserId(userId);

			var response = _postMapper.ToPostResponse(post, user, avt);

			return response;
		}

		public async Task<List<PostResponse>> GetPagedPosts(int page = 1, int pageSize = 10)
		{
			var userId = _userContext.UserId;
			var posts = await _unitOfWork.PostRepository.GetPagedPost(userId, page, pageSize);

			if (posts.Count == 0) return new List<PostResponse>();

			return posts;
		}
	}
}