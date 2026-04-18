using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
	public class RegisterResponse
	{
		public string? Email { get; set; }
		public required RoleTypeEnum Role { get; set; }
	}
}