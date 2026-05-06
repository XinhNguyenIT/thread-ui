using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly ThreadDbContext _context;
	private IDbContextTransaction? _transaction;

	public IUserRepository UserRepository { get; }
	public IRefreshTokenRepository RefreshTokenRepository { get; }
	public IRoleRepository RoleRepository { get; }

	public IPostRepository PostRepository { get; }

	public IMediaRepository MediaRepository { get; }
	public ILikeRepository LikeRepository { get; }
	public ICommentRepository CommentRepository { get; }
	public IStoryRepository StoryRepository { get; }

	public UnitOfWork(ThreadDbContext context, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IRoleRepository roleRepository, IPostRepository postRepository, IMediaRepository mediaRepository, ILikeRepository likeRepository, ICommentRepository commentRepository, IStoryRepository storyRepository)
	{
		_context = context;
		UserRepository = userRepository;
		RefreshTokenRepository = refreshTokenRepository;
		RoleRepository = roleRepository;
		PostRepository = postRepository;
		MediaRepository = mediaRepository;
		LikeRepository = likeRepository;
		CommentRepository = commentRepository;
		StoryRepository = storyRepository;
	}

	public async Task BeginTransactionAsync()
	{
		if (_context.Database.CurrentTransaction != null) return;

		_transaction = await _context.Database.BeginTransactionAsync();
	}

	public async Task CommitAsync()
	{
		try
		{
			await _context.SaveChangesAsync();
			if (_transaction != null) await _transaction.CommitAsync();
		}
		catch
		{
			await RollbackAsync();
			throw;
		}
		finally
		{
			if (_transaction != null)
			{
				await _transaction.DisposeAsync();
				_transaction = null;
			}
		}
	}

	public async Task RollbackAsync()
	{
		if (_transaction != null)
		{
			await _transaction.RollbackAsync();
			await _transaction.DisposeAsync();
			_transaction = null;
		}
	}

	public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

	public void Dispose()
	{
		_context.Dispose();
		_transaction?.Dispose();
	}
}