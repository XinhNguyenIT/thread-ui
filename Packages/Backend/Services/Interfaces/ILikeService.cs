using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;

namespace Backend.Services.Interfaces
{
	public interface ILikeService
	{
		Task UserLikeTarget(int targetId, TargetTypeEnum targetType);
	}
}