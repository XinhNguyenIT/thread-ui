using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Models;

namespace Backend.Mappers
{
	public class UserMapper
	{
		public static User ToModel(RegisterRequest user)
		{
			return new User
			{
				UserName = user.Email,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
			};
		}

		public static AuthInternal ToAuthInternal(User user, List<RoleTypeEnum> roles, List<TokenReturn> tokens)
		{
			return new AuthInternal
			{
				Email = user.Email,
				Roles = roles,
				Tokens = tokens,
			};
		}

		public static AuthResponse ToAuthResponse(AuthInternal user)
		{
			return new AuthResponse
			{
				Email = user.Email,
				Role = user.Roles,
			};
		}
	}
}