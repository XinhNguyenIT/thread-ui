using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Models
{
	public class Media
	{
		public int MediaId { get; set; }

		public string Src { get; set; }
		public string? ProcessedSrc { get; set; }
		public string Type { get; set; }

		public int? CommentId { get; set; }

		public int? PostId { get; set; }
		public MediaStatusEnum Status { get; set; }

		public virtual Comment? Comment { get; set; }

		public virtual Post? Post { get; set; }
	}
}