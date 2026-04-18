using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class Friendship
	{
		public int FriendshipId { get; set; }
		public required string FromUserId { get; set; }
		public required string ToUserId { get; set; }
		public required bool IsFriend { get; set; } = false;
		public virtual User? ToUser { get; set; }
		public virtual User? FromUser { get; set; }

	}
}