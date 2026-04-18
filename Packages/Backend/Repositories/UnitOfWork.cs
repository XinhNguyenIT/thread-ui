using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Backend.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly ThreadDbContext _context;
	private IDbContextTransaction? _transaction;

	public UnitOfWork(ThreadDbContext context)
	{
		_context = context;
	}

	public async Task BeginTransactionAsync()
	{
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