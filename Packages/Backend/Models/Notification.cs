using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
	public class Notification
	{
		public int NotificationId { get; set; }

		public NotificationTypeEnum Type { get; set; }

		public bool IsReaded { get; set; } = false;

		public int? PostId { get; set; }
		public int? StoryId { get; set; }


		public required string FromUserId { get; set; }

		public required string ToUserId { get; set; }

		public virtual Post? Post { get; set; }
		public virtual Post? Story { get; set; }

		public virtual User? FromUser { get; set; }

		public virtual User? ToUser { get; set; }
	}
}