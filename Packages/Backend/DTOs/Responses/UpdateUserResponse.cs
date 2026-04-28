using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
	public class UpdateUserResponse
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public GenderTypeEnum Gender { get; set; }
	}
}