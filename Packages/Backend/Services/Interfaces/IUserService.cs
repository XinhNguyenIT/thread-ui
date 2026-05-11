using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces
{
	public interface IUserService
	{
		Task<UpdateAvatarResponse> UpdateAvatar(UpdateAvatarRequest request);
		Task<UpdateUserResponse> Update(UpdateUserRequest request);
	}
}