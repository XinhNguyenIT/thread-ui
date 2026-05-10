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
		public int? ParentCommentId { get; set; }

		public required string UserId { get; set; }
		public int LikeCount { get; set; } = 0;


		public required int PostId { get; set; }

		public virtual Post? Post { get; set; }

		public virtual User? User { get; set; }

		public virtual ICollection<Media> Medias { get; set; } = new List<Media>();
		public virtual Comment ParentComment { get; set; }

		public virtual ICollection<Comment> ChildComments { get; set; } = new List<Comment>();
	}
}