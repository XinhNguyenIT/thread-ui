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

		public MediaResponse ToMediaResponse(Media media)
		{
			var src = (media.Status == MediaStatusEnum.PROCESSING || media.Status == MediaStatusEnum.FAILED) ? media.Src : media.ProcessedSrc;
			return new MediaResponse
			{
				Id = media.MediaId,
				Src = src,
			};
		}
	}
}