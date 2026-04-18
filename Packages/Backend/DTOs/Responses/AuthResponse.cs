using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.Enums;

namespace Backend.DTOs.Responses
{
	public class AuthResponse
	{
		public string? Email { get; set; }
		public required List<RoleTypeEnum> Role { get; set; }
	}
}