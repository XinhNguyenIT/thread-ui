using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public static RegisterResponse ToRegisterResponse(User user, RoleTypeEnum role)
		{
			return new RegisterResponse
			{
				Email = user.Email,
				Role = role,
			};
		}
	}
}