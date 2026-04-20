using Backend.Enums;

namespace Backend.Services.Interfaces;

public interface IUrlService
{
	string GetFullUrl(string? fileName, GenderTypeEnum gender);
}