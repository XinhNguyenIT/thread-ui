using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Requests
{
	public class CreatePostRequest
	{
		public string? Caption { get; set; }
		public PrivacySettingEnum PrivacySetting { get; set; }
		public bool IsAvatar { get; set; }
		public List<string> Files { get; set; } = new List<string>();
	}
}