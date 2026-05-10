using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.Responses
{
	public class CreatePostResponse
	{
		public required UserBasicResponse Author { get; set; }
		public string? Caption { get; set; }
		public int LikesCount { get; set; }
		public int CommentsCount { get; set; }
		public DateTime CreateAt { get; set; }
		public List<string> MediaUrls { get; set; } = new List<string>();
	}
}