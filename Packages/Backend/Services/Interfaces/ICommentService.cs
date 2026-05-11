using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.Requests;
using Backend.DTOs.Responses;

namespace Backend.Services.Interfaces
{
	public interface ICommentService
	{
		public Task<List<CommentResponse>> GetCommentAsync(GetCommentRequest request);
		public Task<CommentResponse> CreateCommentAsync(CreateCommentRequest request);
	}
}