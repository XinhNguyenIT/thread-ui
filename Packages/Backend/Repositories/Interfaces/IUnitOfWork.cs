using Backend.Models;

namespace Backend.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
	IUserRepository UserRepository { get; }
	IRefreshTokenRepository RefreshTokenRepository { get; }
	IRoleRepository RoleRepository { get; }
	IPostRepository PostRepository { get; }
	IMediaRepository MediaRepository { get; }

	Task BeginTransactionAsync();
	Task CommitAsync();
	Task RollbackAsync();
	Task<int> SaveChangesAsync();
}