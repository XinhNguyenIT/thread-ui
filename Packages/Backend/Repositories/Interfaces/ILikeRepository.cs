using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Enums;
using Backend.Models;

namespace Backend.Repositories.Interfaces
{
	public interface ILikeRepository : IGenericRepository<Like>
	{
		Task UserLikeTarget(string userId, int targetId, TargetTypeEnum targetType);
		Task<Like?> GetLikeByUserIdAndTargetId(string userId, int targetId, TargetTypeEnum targetType);
		Task<List<Like>> GetLikeByUserIdAndTargetIds(string userId, IEnumerable<int> targetIds, TargetTypeEnum targetType);
		Task<Dictionary<int, int>> GetTargetLikeCountsAsync(List<int> commentIds, TargetTypeEnum targetType);
	}
}