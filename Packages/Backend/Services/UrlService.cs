
using Backend.Enums;
using Backend.Models;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class UrlService : IUrlService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UrlService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetFullUrlDefault(Media media)
	{
		var request = _httpContextAccessor.HttpContext?.Request;
		if (request == null) return media.Src;

		string fileName;
		string folderName;
		string subFolderName = "";

		folderName = "uploads";
		var mediaIsDone = media.Status == MediaStatusEnum.DONE;
		fileName = media.Src;

		subFolderName = mediaIsDone ? "posts" : "temps";

		if (mediaIsDone && !string.IsNullOrEmpty(media.ProcessedSrc))
		{
			fileName = media.ProcessedSrc;
		}

		if (fileName.StartsWith("https")) return fileName;

		var parts = new List<string> { folderName, subFolderName, fileName }
						.Where(s => !string.IsNullOrWhiteSpace(s));

		var path = string.Join("/", parts);

		var baseUrl = $"{request.Scheme}://{request.Host}";
		return $"{baseUrl}/{path}";
	}

	public string GetFullUrlForAvatar(Media media, GenderTypeEnum gender = GenderTypeEnum.UNKNOWN)
	{
		var request = _httpContextAccessor.HttpContext?.Request;
		if (request == null) return media.Src;

		string fileName;
		string folderName;
		string subFolderName = "";

		if (media == null || string.IsNullOrEmpty(media.Src))
		{
			folderName = "images";
			fileName = (gender == GenderTypeEnum.FEMALE) ? "Female.jpg" : "Male.jpg";
		}
		else
		{
			folderName = "uploads";
			var mediaIsDone = media.Status == MediaStatusEnum.DONE;
			fileName = media.Src;

			subFolderName = mediaIsDone ? "posts" : "temps";

			if (mediaIsDone && !string.IsNullOrEmpty(media.ProcessedSrc))
			{
				fileName = media.ProcessedSrc;
			}
		}

		var parts = new List<string> { folderName, subFolderName, fileName }
						.Where(s => !string.IsNullOrWhiteSpace(s));

		if (fileName.StartsWith("https")) return fileName;

		var path = string.Join("/", parts);

		var baseUrl = $"{request.Scheme}://{request.Host}";
		return $"{baseUrl}/{path}";
	}
}