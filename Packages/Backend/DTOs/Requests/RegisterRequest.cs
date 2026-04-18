using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.DTOs.Requests
{
	public class RegisterRequest
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email is not valid")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
			ErrorMessage = "Password must contain uppercase, lowercase, number and special character")]
		public required string Password { get; set; }
		public RoleTypeEnum Role { get; set; } = RoleTypeEnum.USER;
	}
}