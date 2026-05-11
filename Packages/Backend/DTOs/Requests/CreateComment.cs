using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Requests
{
	public class CreateCommentRequest
	{
		public int? CommentId { get; set; }
		public int? PostId { get; set; }
		public string content { get; set; }
	}
}