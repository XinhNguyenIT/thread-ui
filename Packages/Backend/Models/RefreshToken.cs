
namespace Backend.Models;

public class RefreshToken
{
	public int Id { get; set; }
	public string Token { get; set; }
	public string UserId { get; set; }
	public DateTime? RevokedAt { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime ExpiryDate { get; set; }
	public bool IsRevoked { get; set; } = false;
	public virtual User User { get; set; }

}