using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class Hashtag
	{
		public int HashtagId { get; set; }
		public required string Content { get; set; }

		public virtual ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
	}
}