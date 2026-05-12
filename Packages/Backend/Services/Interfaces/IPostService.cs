using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces
{
	public interface IPostService
	{
		public Task<PostResponse> CreatePost(CreatePostRequest request);
		public Task DeletePost(DeletePostRequest request);
		public Task<List<PostResponse>> GetPagedPosts(int page = 1, int pageSize = 10);
	}
}