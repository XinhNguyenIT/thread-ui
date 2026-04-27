using Backend.Enums;
using Backend.Models;

namespace Backend.Dataset.Interfaces;

public interface ISeedData
{
	string Type { get; }
	List<string> Roles { get; }
	List<(User user, string password, List<RoleTypeEnum> roles)> Users { get; }
}