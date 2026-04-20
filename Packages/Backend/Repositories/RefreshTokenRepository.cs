using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class RefreshTokenRepository : IGenericRepository<RefreshToken>, IRefreshTokenRepository
	{
		private readonly ThreadDbContext _context;

		public RefreshTokenRepository(ThreadDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(RefreshToken entity)
		{
			await _context.AddAsync(entity);
		}

		public void Delete(RefreshToken entity)
		{
			throw new NotImplementedException();
		}

		public async Task<List<RefreshToken>> FindByUserId(string userId)
		{
			return await _context.RefreshTokens
									.Where(t => !t.IsRevoked && t.UserId == userId)
									.ToListAsync();
		}

		public Task<IEnumerable<RefreshToken>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<RefreshToken?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task RevokeToken(int tokenId)
		{
			var token = await _context.RefreshTokens.FindAsync(tokenId) ?? throw new BadHttpRequestException("Token not found");


			token.IsRevoked = true;
			token.RevokedAt = DateTime.UtcNow;

			var effectedRows = await _context.SaveChangesAsync();

			if (effectedRows <= 0) throw new BadHttpRequestException("Failed to revoke token");
		}

		public void Update(RefreshToken entity)
		{
			throw new NotImplementedException();
		}
	}
}