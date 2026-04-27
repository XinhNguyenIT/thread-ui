using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories.Interfaces
{
	public interface IPostRepository : IGenericRepository<Post>
	{
		public List<Post> GetPagedPost(int page = 1, int pageSize = 10);
	}
}