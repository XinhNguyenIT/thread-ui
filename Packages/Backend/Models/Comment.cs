using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class Comment
	{
		public int CommentId { get; set; }
		public required string Content { get; set; }
		public int? ParentComment { get; set; }

		public required string UserId { get; set; }

		public required int PostId { get; set; }

		public virtual Post? Post { get; set; }

		public virtual User? User { get; set; }

		public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
	}
}