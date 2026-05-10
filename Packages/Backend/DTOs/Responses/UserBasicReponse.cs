using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
	public class UserBasicResponse
	{
		public string? UserId { get; set; }
		public string? LastName { get; set; }
		public string? FirstName { get; set; }
		public GenderTypeEnum Gender { get; set; }
		public MediaResponse? Avatar { get; set; }
	}
}