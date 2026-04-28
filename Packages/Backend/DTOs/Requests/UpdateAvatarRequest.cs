using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Requests
{
	public class UpdateAvatarRequest
	{
		public string? File { get; set; }
		public PrivacySettingEnum Privacy { get; set; } = PrivacySettingEnum.PRIVATE;
	}
}