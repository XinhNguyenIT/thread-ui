using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Backend.Common
{
	public class ApiResponse<T>
	{
		public bool Success { get; set; }
		public int StatusCode { get; set; }
		public string Message { get; set; } = string.Empty;
		public T? Data { get; set; }

		public static ApiResponse<T> SuccessResponse(
			T data,
			string message = "Success",
			HttpStatusCode statusCode = HttpStatusCode.OK)
		{
			return new ApiResponse<T>
			{
				Success = true,
				StatusCode = (int)statusCode,
				Message = message,
				Data = data
			};
		}
	}
}