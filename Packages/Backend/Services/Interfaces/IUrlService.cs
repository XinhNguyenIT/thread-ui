using Backend.Enums;
using Backend.Models;

namespace Backend.Services.Interfaces;

public interface IUrlService
{
	string GetFullUrlForAvatar(Media media, GenderTypeEnum gender = GenderTypeEnum.UNKNOWN);
	string GetFullUrlDefault(Media media, bool isTemp = false);
}