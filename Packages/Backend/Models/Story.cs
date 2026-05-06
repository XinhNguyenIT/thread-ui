using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class Story
	{
		public int StoryId { get; set; }
		public required string Src { get; set; }
		public required string UserId { get; set; }
		public virtual User? User { get; set; }
		public int LikeCount { get; set; } = 0;

		public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

	}
}