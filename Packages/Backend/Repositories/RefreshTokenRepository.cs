using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
	public class RefreshTokenRepository : IGenericRepository<RefreshToken>, IRefreshTokenRepository
	{
		private readonly ThreadDbContext _context;
		private readonly IUnitOfWork _unitOfWork;

		public RefreshTokenRepository(ThreadDbContext context, IUnitOfWork unitOfWork)
		{
			_context = context;
			_unitOfWork = unitOfWork;
		}

		public async Task AddAsync(RefreshToken entity)
		{
			await RevokeToken(entity.UserId);

			await _context.AddAsync(entity);

			await _context.SaveChangesAsync();
		}

		public void Delete(RefreshToken entity)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<RefreshToken>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<RefreshToken?> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task RevokeToken(string userId)
		{
			var tokens = await _context.RefreshTokens
									.Where(t => !t.IsRevoked && t.UserId == userId)
									.ToListAsync();

			if (tokens.Any())
			{
				await _unitOfWork.BeginTransactionAsync();
				try
				{
					foreach (var t in tokens)
					{
						t.IsRevoked = true;
						t.RevokedAt = DateTime.UtcNow;
					}

					await _unitOfWork.CommitAsync();
				}
				catch (Exception)
				{
					await _unitOfWork.RollbackAsync();
					throw;
				}


			}
		}

		public void Update(RefreshToken entity)
		{
			throw new NotImplementedException();
		}
	}
}