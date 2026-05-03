using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Internals;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;
using Backend.Enums;
using Backend.Models;
using Backend.Services.Interfaces;

namespace Backend.Mappers
{
	public class UserMapper
	{
		private readonly IUrlService _urlService;

		public UserMapper(IUrlService urlService)
		{
			_urlService = urlService;
		}

		public User ToModel(RegisterRequest user)
		{
			return new User
			{
				UserName = user.Email,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
			};
		}

		public User ToModel(UpdateUserRequest user)
		{
			return new User
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Gender = user.Gender,
			};
		}

		public AuthInternal ToAuthInternal(User user, List<RoleTypeEnum> roles, List<TokenReturn> tokens, Media? avtSrc = null)
		{
			return new AuthInternal
			{
				Email = user.Email,
				Roles = roles,
				Tokens = tokens,
				FirstName = user.FirstName,
				LastName = user.LastName,
				AvatarSrc = avtSrc,
				Gender = user.Gender
			};
		}

		public UserResponse ToAuthResponse(AuthInternal user)
		{
			return new UserResponse
			{
				Email = user.Email,
				Role = user.Roles,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Gender = user.Gender,
				AvatarSrc = _urlService.GetFullUrl(user.AvatarSrc, user.Gender)
			};
		}

		public UpdateUserResponse ToUpdateUserResponse(User user)
		{
			return new UpdateUserResponse
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Gender = user.Gender,
			};
		}

		public UserCreatePostResponse ToUserCreatePostResponse(User user, Media? avatar)
		{
			return new UserCreatePostResponse
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Gender = user.Gender,
				AvatarSrc = _urlService.GetFullUrl(avatar, user.Gender)
			};
		}
	}
}