using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories.Interfaces
{
	public interface IRefreshTokenRepository
	{
		public Task RevokeToken(int token);
		public Task<List<RefreshToken>> FindByUserId(string userId);
	}
}