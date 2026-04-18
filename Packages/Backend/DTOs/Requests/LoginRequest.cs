using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Requests;

public class LoginRequest
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Email is not valid")]
	public required string Email { get; set; }

	public required string Password { get; set; }
}