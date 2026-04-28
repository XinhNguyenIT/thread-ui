using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories.Interfaces
{
	public interface IPostRepository : IGenericRepository<Post>
	{
		List<Post> GetPagedPost(int page = 1, int pageSize = 10);
		Task DisableAllAvatarsAsync(string userId);
	}
}