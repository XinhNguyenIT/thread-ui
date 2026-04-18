using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
	public class PostHashtag
	{
		public int PostId { get; set; }
		public int HashtagId { get; set; }

		public virtual Post? Post { get; set; }
		public virtual Hashtag? Hashtag { get; set; }
	}
}