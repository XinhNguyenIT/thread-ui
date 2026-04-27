using Backend.Background.Queue;
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

			foreach (var temp in request.Files)
			{
				var finalName = await _fileService.MoveToPermanentAsync(temp);

				medias.Add(new Media
				{
					Src = finalName,
					Type = GetTypeFromExtension(finalName),
					Status = MediaStatusEnum.PROCESSING
				});
			}

			var post = _postMapper.ToModel(request, userId, medias);

			await _unitOfWork.PostRepository.AddAsync(post);
			await _unitOfWork.CommitAsync();

			foreach (var m in medias)
			{
				_mediaQueue.Enqueue(m.MediaId);
			}

			var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
			var avt = await _unitOfWork.MediaRepository.GetAvtSrcByUserId(userId);

			var response = _postMapper.ToPostResponse(post, user, avt);

			return response;
		}

		private string GetTypeFromExtension(string finalName)
		{
			var extension = Path.GetExtension(finalName).ToLowerInvariant();

			return extension switch
			{
				// Nhóm Image
				".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" => "image",

				// Nhóm Video
				".mp4" or ".mov" or ".avi" or ".wmv" or ".flv" or ".mkv" => "video",

				// Các loại khác hoặc mặc định
				".pdf" => "pdf",
				_ => "other"
			};
		}
	}
}