using Backend.Enums;
using Backend.Models;

namespace Backend.Services.Interfaces;

public interface IUrlService
{
	string? GetFullUrl(Media? media, GenderTypeEnum gender);
}