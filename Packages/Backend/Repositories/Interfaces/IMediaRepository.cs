using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repositories.Interfaces
{
	public interface IMediaRepository : IGenericRepository<Media>
	{
		Task<Media> GetAvtSrcByUserId(string userId);
		Task<List<Media>> GetByPostId(int postId);
	}
}