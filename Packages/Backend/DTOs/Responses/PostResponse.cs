using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
	public class PostResponse
	{
		public int PostId { get; set; }
		public required UserCreatePostResponse Author { get; set; }
		public string? Caption { get; set; }
		public PrivacySettingEnum PrivacySetting { get; set; }
		public int LikesCount { get; set; } = 0;
		public int CommentsCount { get; set; } = 0;
		public bool IsAvatar { get; set; }
		public DateTime CreateAt { get; set; }
		public List<MediaResponse> Medias { get; set; } = new List<MediaResponse>();
	}
}