using Backend.Enums;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
	public class User : IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime Birthday { get; set; }
		public GenderTypeEnum Gender { get; set; } = GenderTypeEnum.UNKNOWN;

		public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
		public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
		public virtual ICollection<Friendship> FriendshipRequests { get; set; } = new List<Friendship>();
		public virtual ICollection<Friendship> FriendshipResponses { get; set; } = new List<Friendship>();
		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
		public virtual ICollection<Story> Stories { get; set; } = new List<Story>();
		public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
		public virtual ICollection<PostReport> CreateReport { get; set; } = new List<PostReport>();
		public virtual ICollection<Notification> SentNotifications { get; set; } = new List<Notification>();
		public virtual ICollection<Notification> ReceiveNotifications { get; set; } = new List<Notification>();
	}
}