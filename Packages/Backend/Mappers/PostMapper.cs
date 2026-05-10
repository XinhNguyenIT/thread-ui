using Azure.Core;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Models;
using Backend.Services.Interfaces;

namespace Backend.Mappers
{
	public class PostMapper
	{
		private readonly IUrlService _urlService;
		private readonly UserMapper _userMapper;
		private readonly MediaMapper _mediaMapper;

		public PostMapper(IUrlService urlService, UserMapper userMapper, MediaMapper mediaMapper)
		{
			_urlService = urlService;
			_userMapper = userMapper;
			_mediaMapper = mediaMapper;
		}

		public Post ToModel(CreatePostRequest request, string userId, List<Media> mediasInput)
		{
			return new Post
			{
				UserId = userId,
				Content = request.Caption,
				PrivacySetting = request.PrivacySetting,
				Medias = mediasInput,
				IsAvatar = request.IsAvatar,
				CreateAt = DateTime.UtcNow.AddHours(7)
			};
		}

		public Post ToModel(UpdateAvatarRequest request, string userId, Media media)
		{
			return new Post
			{
				UserId = userId,
				PrivacySetting = request.Privacy,
				Medias = new List<Media> { media },
				IsAvatar = true
			};
		}

		public PostResponse ToPostResponse(Post post)
		{
			var medias = post.Medias.Select(m => _mediaMapper.ToMediaResponse(m)).ToList();
			return new PostResponse
			{
				Author = _userMapper.ToUserBasicResponse(post.Author, post.Author.Posts.FirstOrDefault(p => p.IsAvatar)?.Medias.FirstOrDefault()),
				Caption = post.Content,
				CreateAt = post.CreateAt,
				Medias = medias,
				PostId = post.PostId,
				PrivacySetting = post.PrivacySetting
			};
		}

		public PostResponse ToPostResponse(Post post, User user, Media? avatar)
		{
			var medias = post.Medias.Select(m => _mediaMapper.ToMediaResponse(m)).ToList();
			return new PostResponse
			{
				Author = _userMapper.ToUserBasicResponse(user, avatar),
				Caption = post.Content,
				CreateAt = post.CreateAt,
				Medias = medias,
				PostId = post.PostId,
				PrivacySetting = post.PrivacySetting
			};
		}
	}
}