using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class Picture
	{
		public int PictureId { get; set; }

		public required string Src { get; set; }

		public int? CommentId { get; set; }

		public int? PostId { get; set; }

		public virtual Comment? Comment { get; set; }

		public virtual Post? Post { get; set; }
	}
}