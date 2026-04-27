using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
	public class Post
	{
		public int PostId { get; set; }

		public string? Content { get; set; }
		public DateTime CreateAt { get; set; } = DateTime.UtcNow;

		public PrivacySettingEnum PrivacySetting { get; set; }

		public bool IsReported { get; set; } = false;

		public required string UserId { get; set; }
		public bool IsAvatar { get; set; } = false;

		public virtual User? Author { get; set; }

		public virtual ICollection<Media> Medias { get; set; } = new List<Media>();

		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
		public virtual ICollection<PostReport> Reports { get; set; } = new List<PostReport>();
		public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

		public virtual ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
	}
}