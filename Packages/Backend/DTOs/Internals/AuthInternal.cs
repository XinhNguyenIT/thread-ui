using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.Enums;

namespace Backend.DTOs.Internals
{
	public class AuthInternal
	{
		public string? Email { get; set; }
		public string? LastName { get; set; }
		public string? FirstName { get; set; }
		public string? AvatarSrc { get; set; }
		public GenderTypeEnum Gender { get; set; }
		public required List<RoleTypeEnum> Roles { get; set; }

		public List<TokenReturn> Tokens { get; set; } = new List<TokenReturn>();
	}
}