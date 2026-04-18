using Backend.DTOs.Internals;
using Backend.Enums;
using Backend.Models;

namespace Backend.Services.Interfaces
{
	public interface IJwtService
	{
		public string RevokeToken(string userId, string token);
		public Task<List<TokenReturn>> CreateTokenForUser(User user, List<RoleTypeEnum> role);

	}
}