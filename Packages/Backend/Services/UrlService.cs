
using Backend.Enums;
using Backend.Services.Interfaces;

namespace Backend.Services;

public class UrlService : IUrlService
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UrlService(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public string GetFullUrl(string? fileName, GenderTypeEnum gender)
	{
		var request = _httpContextAccessor.HttpContext?.Request;
		if (request == null) return fileName;

		var folderName = "uploads";

		if (string.IsNullOrEmpty(fileName))
		{
			fileName = "Male.jpg";
			folderName = "images";
			if (gender == GenderTypeEnum.Female)
			{
				fileName = "Female.jpg";
			}
		}

		var baseUrl = $"{request.Scheme}://{request.Host}";
		return $"{baseUrl}/{folderName}/{fileName}";
	}
}