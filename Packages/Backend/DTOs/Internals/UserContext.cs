using Backend.Enums;

namespace Backend.DTOs.Internals;

public class UserContext
{
	public string UserId { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public List<RoleTypeEnum> Roles { get; set; } = new List<RoleTypeEnum>();
}