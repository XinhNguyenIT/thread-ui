using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Requests
{
	public class CreateCommentRequest
	{
		public int? CommentId { get; set; } = null;
		public int? PostId { get; set; } = null;
		public string content { get; set; }
	}
}