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

		public PrivacySettingEnum PrivacySetting { get; set; } = PrivacySettingEnum.PUBLIC;

		public bool IsReported { get; set; }

		public required string UserId { get; set; }

		public virtual required User Author { get; set; }

		public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
		public virtual ICollection<PostReport> Reports { get; set; } = new List<PostReport>();
		public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

		public virtual ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
	}
}