using Azure.Core;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Models;
using Backend.Services.Interfaces;

namespace Backend.Mappers
{
	public class MediaMapper
	{
		private readonly IUrlService _urlService;

		public MediaMapper(IUrlService urlService)
		{
			_urlService = urlService;
		}

		public Post ToModel(CreatePostRequest request, string userId)
		{
			return new Post
			{
				UserId = userId,
				Content = request.Caption,
				PrivacySetting = request.PrivacySetting
			};

		}

		public MediaResponse ToMediaResponse(Media media, GenderTypeEnum? gender = null)
		{
			return new MediaResponse
			{
				Id = media.MediaId,
				Type = media.Type,
				Src = gender != null
						? _urlService.GetFullUrlForAvatar(media, gender.Value)
						: _urlService.GetFullUrlDefault(media)
			};
		}

		public UpdateAvatarResponse ToAvatarResponse(Media media, GenderTypeEnum? gender = null)
		{
			return new UpdateAvatarResponse
			{
				AvatarSrc = gender != null
						? _urlService.GetFullUrlForAvatar(media, gender.Value)
						: _urlService.GetFullUrlDefault(media)
			};
		}
	}
}