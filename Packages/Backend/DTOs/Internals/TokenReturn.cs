using Backend.Enums;

namespace Backend.DTOs.Internals;

public class TokenReturn
{
	public TokenTypeEnum Type { get; set; }
	public required string Token { get; set; }
	public DateTime Expires { get; set; }
}