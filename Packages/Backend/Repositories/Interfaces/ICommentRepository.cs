using Backend.Models;

namespace Backend.Repositories.Interfaces
{
	public interface ICommentRepository : IGenericRepository<Comment>
	{
		Task<List<Comment>> GetCommentByPostIdAsync(int postId);
		Task<List<Comment>> GetCommentByParentCommentIdAsync(int parentCommentId);
	}
}